import { AfterViewInit, Component, computed, ElementRef, forwardRef, inject, input, OnInit, signal, viewChild } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SafeHtml } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { v4 as uuidv4 } from 'uuid';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
import { MarkdownSelectionDetails } from '../../../models/markdown.model';
import { MarkdownService } from 'sloth-utilities';

@Component({
  selector: 'sl-markdown-input',
  imports: [ReactiveFormsModule, MatInputModule, FormsModule, MatTooltipModule, MatButtonModule],
  templateUrl: './markdown-input.component.html',
  styleUrl: './markdown-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MarkdownInputComponent),
      multi: true
    }
  ],
})
export class MarkdownInputComponent extends BaseFormControlComponent implements OnInit, AfterViewInit {
  private readonly markdownService = inject(MarkdownService);
  markdownInput = viewChild('markdown', { read: ElementRef<HTMLTextAreaElement> });
  
  name = input<string>(this.formControlName() ?? uuidv4());
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  showLabelOnReadModel = input<boolean>(false);
  isFullView = input<boolean>(false);

  sanitizedValue = signal<SafeHtml>('');
  isPreview = signal<boolean>(false);
  hideTooltip = computed(() => !this.tooltip());
  modeSelectionText = computed(() => this.isPreview() ? this.prompts().markdown : this.prompts().preview);
  valueHistory: string[] = [];

  override async ngOnInit(): Promise<void> {
    super.ngOnInit();

    this.sanitizedValue.set(await this.markdownService.sanitizeValue(this.value()));

    this.value.subscribe(async (value)=> {
      this.sanitizedValue.set(await this.markdownService.sanitizeValue(value));
    })
   
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
    strikethrough: 'Strikethrough',
    markdown: 'Markdown'
  });

  toggleMode(): void {
    this.isPreview.set(!this.isPreview());
  }

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

  private getSelectionDetails(): MarkdownSelectionDetails {
    const startIndex = this.markdownInput()!.nativeElement.selectionStart;
    const endIndex = this.markdownInput()!.nativeElement.selectionEnd;
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
      this.markdownInput()!.nativeElement.setSelectionRange(newStartIndex, newEndIndex);
    },1);
  }

  private doesWordContainMarker(word: string, marker: string): boolean {
    let startsWithMarker = word.startsWith(marker);
    let endsWithMarker = word.endsWith(marker);
  
    return startsWithMarker && endsWithMarker;
  }

  private toggleTwoSidedMarker (marker: string, details: MarkdownSelectionDetails) {
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

  onValueChange(value: string) : void {
    this.valueHistory.push(value);
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

