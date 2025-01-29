import { AfterViewInit, Component, computed, ElementRef, forwardRef, inject, input, OnInit, signal, ViewChild, viewChild } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { v4 as uuidv4 } from 'uuid';
import { marked } from 'marked';
import DOMPurify from 'dompurify';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
import { MarkupSelectionDetails } from '../../../models/markup.model';

@Component({
  selector: 'sl-markup-input',
  imports: [ReactiveFormsModule, MatInputModule, FormsModule, MatTooltipModule],
  templateUrl: './markup-input.component.html',
  styleUrl: './markup-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MarkupInputComponent),
      multi: true
    }
  ],
})
export class MarkupInputComponent extends BaseFormControlComponent implements OnInit, AfterViewInit {
  // markupInnerInput = viewChild('markup', { read: ElementRef });
  // markupInput = computed(()=> this.markupInnerInput()!.nativeElement)
  private readonly sanitizer = inject(DomSanitizer);
  name = input<string>(this.formControlName() ?? uuidv4());
  sanitizedValue = signal<SafeHtml>('');
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());
  valueHistory: string[] = [];

  @ViewChild('markup', { read: ElementRef }) markupInput!: ElementRef<HTMLTextAreaElement>;

  override ngOnInit(): void {
    super.ngOnInit();

    this.sanitizeValue();
  }

  ngAfterViewInit(): void {
    document.addEventListener('click', (event) => {
      const target = event.target as HTMLElement;

      const button = target.closest('.copy-button button');
      if (button) {
        this.copyCodeToClipboard(button);
        return; 
      }
  
      if (target.tagName === 'A' && target.hasAttribute('href')) {
        const href = target.getAttribute('href');
        if (href && !href.startsWith('/')) {
          event.preventDefault(); 
          window.open(href, '_blank');
        }
      }
    });
  }

  public prompts = signal<any>({
    required: 'Required',
    write: 'Write',
    preview: 'Preview',
    header: 'Header',
    bold: 'Bold',
    italic: 'Italic',
    quote: 'Quote',
    codeBlock: 'Code block',
    url: 'Url',
    numberedList: 'Numbered list',
    bulletList: 'Bullet list',
    addFile: 'Add file',
    addImage: 'Add image',
    strikethrough: 'Strikethrough'
  });

  toggleHeader(): void {
      const details = this.getSelectionDetails();
      let indexShift = 0;
      let line = this.readLine(details.startLine);
      const pattern = /^(#{1,5})\s/;
      const startsWithHeading= pattern.test(line);
      if(startsWithHeading) {
        if(line.startsWith("##### ")) {
          line = line.substring(6);
          indexShift = -6
        }
        else {
          line = "#" + line;
          indexShift = 1
        }
      }
      else {
        line = "# " + line;
        indexShift = 2
      }

      this.appendLine(line, details.startLine);
      this.setSelection(details.startIndex, details.endIndex, indexShift)
  }

  toggleCodeBlock(): void {
    let details = this.getSelectionDetails();
    if(details.startLine === details.endLine)
      this.toggleTwoSidedMarker('`', details);
    else 
      this.toggleTwoSidedMarker('\n```\n', details);
  }

  toggleBold(): void {
    let details = this.getSelectionDetails();
    this.toggleTwoSidedMarker('**', details);
  }

  toggleItalic(): void {
    let details = this.getSelectionDetails();
    this.toggleTwoSidedMarker('_', details);
  }
  
  toggleStrikethrough(): void {
    let details = this.getSelectionDetails();
    this.toggleTwoSidedMarker('~~', details);
  }

  private getSelectionDetails(): MarkupSelectionDetails {
    const startIndex = this.markupInput.nativeElement.selectionStart;
    const endIndex = this.markupInput.nativeElement.selectionEnd;
    const selectionText = this.value().substring(startIndex, endIndex);
    const beforeText = this.value().substring(0, startIndex);
    const afterText = this.value().substring(endIndex, this.value().length);
    let isSingleIndex: boolean = false;
    let startLine = 0;
    let endLine = 0;
    if(startIndex === endIndex) {
      isSingleIndex = true;
      startLine = endLine = this.getSelectionLine(startIndex);

    }
    else {
      startLine = this.getSelectionLine(startIndex);
      endLine = this.getSelectionLine(endIndex);
    }

    return {
      startIndex: startIndex,
      endIndex: endIndex,
      selectionText: selectionText,
      beforeText: beforeText,
      afterText: afterText,
      isSingleIndex: isSingleIndex,
      startLine: startLine,
      endLine: endLine
    }
  }

  private getSelectionLine(index: number): number {
    return this.value().substring(0, index).split('\n').length - 1;
  }

  private setSelection(startIndex: number, endIndex: number, indexShift: number){
    const newStartIndex = startIndex + indexShift;
    const newEndIndex = endIndex + indexShift;

    setTimeout(() => {
      this.markupInput.nativeElement.setSelectionRange(newStartIndex, newEndIndex);
    },1);
  }

  private doesWordContainMarker(word: string, marker: string): boolean {
    let startsWithMarker = word.startsWith(marker);
    let endsWithMarker = word.endsWith(marker);
  
    return startsWithMarker && endsWithMarker;
  }

  private toggleTwoSidedMarker (marker: string, details: MarkupSelectionDetails) {
    let { startIndex, endIndex, selectionText, beforeText, afterText } = details;
    let originalStartIndex = startIndex;
    let originalEndIndex = endIndex;
    let isMarker = false;
    
    // check if cursor at word
    if(details.isSingleIndex) {
      const adjustedCursor = startIndex === this.value().length ? startIndex - 1 : startIndex;
      if (!(/\s/.test(this.value().charAt(adjustedCursor)))) {
        let wordStart = adjustedCursor;
        while (wordStart > 0 && !/\s/.test(this.value().charAt(wordStart - 1))) {
          wordStart--;
        }

        let wordEnd = adjustedCursor;
        while (wordEnd < this.value().length && !/\s/.test(this.value().charAt(wordEnd))) {
          wordEnd++;
        }

        let word = this.value().substring(wordStart, wordEnd);

        // Determine if the word is already wrapped with the marker
        const isAlreadyMarked = this.doesWordContainMarker(word, marker);

        if (isAlreadyMarked) {
          // Remove the marker
          word = word.substring(marker.length, word.length - marker.length);
          wordStart = wordStart + marker.length;
          wordEnd = wordEnd - marker.length;
        } 

        selectionText = word;
        startIndex = wordStart;
        endIndex = wordEnd;
        beforeText = this.value().substring(0, startIndex);
        afterText = this.value().substring(endIndex, this.value().length);
      }
    }
    // check if markers were selected
    else if(this.doesWordContainMarker(selectionText, marker)){
      originalStartIndex += marker.length;
      originalEndIndex -= marker.length;
      selectionText = selectionText.replaceAll(marker, '');
      isMarker = true;
    }
    else if(this.doesWordContainMarker(selectionText.replaceAll('\n', ''), marker.replaceAll('\n', ''))) {
      originalStartIndex += marker.length - 1;
      originalEndIndex -= marker.length - 1;
      selectionText = this.replaceFirstAndLast(selectionText, '\n', '')
      selectionText = this.replaceFirstAndLast(selectionText, marker.replaceAll('\n', ''), '')
      beforeText = beforeText.substring(0, beforeText.length - 1);
      afterText = afterText.substring(1, afterText.length);
      isMarker = true;
    }
  
    let testValue = this.value().substring(startIndex - marker.length, endIndex + marker.length);
    if(this.doesWordContainMarker(testValue, marker) || isMarker) {
      if(!isMarker) {
        beforeText = beforeText.substring(0, beforeText.length-marker.length);
        afterText = afterText.substring(marker.length, afterText.length);
      }
      this.setNewValue(`${beforeText}${selectionText}${afterText}`);
      this.setSelection(originalStartIndex, originalEndIndex, -marker.length);
    }
    else {
      const leftWhitespace = this.getLeftWhitespace(selectionText);
      originalStartIndex += leftWhitespace.length;
      const rightWhitespace = this.getRightWhitespace(selectionText);
      originalEndIndex -= rightWhitespace.length;
      selectionText = selectionText.trim();
      beforeText =`${beforeText}${leftWhitespace}${marker}`
      afterText = `${marker}${rightWhitespace}${afterText}`
      this.setNewValue(`${beforeText}${selectionText}${afterText}`);
      this.setSelection(originalStartIndex, originalEndIndex, marker.length);
    }
  }

  private appendLine(value: string, lineID: number): void {
    const lines = this.value().split('\n');
    lines[lineID] = value;
    const newValue = lines.join('\n');
    this.setNewValue(newValue);
  }

  private readLine(lineID: number): string {
    return this.value().split('\n')[lineID];
  }

  private getRightWhitespace(value: string): string {
    const match = value.match(/\s+$/);
    return match ? match[0] : '';
  }
  
  private getLeftWhitespace(value: string): string {
    const match = value.match(/^\s+/);
    return match ? match[0] : '';
  }

  private replaceFirstAndLast(value: string, target: string, replacement: string) {
    let newStr = value.replace(target, replacement);
    
    const lastIndex = newStr.lastIndexOf(target);
    if (lastIndex !== -1) {
        newStr = newStr.substring(0, lastIndex) + replacement + newStr.substring(lastIndex + target.length);
    }
    
    return newStr;
  }

  private setNewValue(value: string) {
    this.value.set(value);
    this.onValueChange(value);
  }

  private async sanitizeValue(): Promise<void> {
    if(!this.value()) {
      this.sanitizedValue.set('');
      return;
    }

    const renderer = new marked.Renderer();

    renderer.code = ({ text, lang }: { text: string; lang?: string }) => {
      const escapedCode = DOMPurify.sanitize(text.trim());
      
      return `
        <div class="custom-code-block">
          <div class="copy-button">
            <button data-code="${escapedCode}">
              <span class="icon-span">content_copy</span>
            </button>
          </div>
          <pre><code class="language-${lang ?? 'plaintext'}">${escapedCode}</code></pre>
        </div>
      `;
    };

    marked.setOptions({ 
      renderer,  
      breaks: true });

    const rawHtml = await marked(this.value());
    const cleanHtml = DOMPurify.sanitize(rawHtml);
    this.sanitizedValue.set(this.sanitizer.bypassSecurityTrustHtml(cleanHtml));
  }

  onValueChange(value: string) {
    this.valueHistory.push(value);
    this.sanitizeValue();
  }

  copyCodeToClipboard(button: Element): void {
    const codeBlock = button.closest('.custom-code-block')?.querySelector('code');
  
    if (codeBlock) {
      const textToCopy = codeBlock.textContent || '';
      navigator.clipboard.writeText(textToCopy).then(() => {
        // Change icon to success check mark
        button.innerHTML = "<span class='icon-span'>done</span>";
        // Revert back after 2 seconds
        setTimeout(() => {
          button.innerHTML = "<span class='icon-span'>content_copy</span>";
        }, 2000);
      });
    }
  }
}

