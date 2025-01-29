import { inject, Injectable } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';
import DOMPurify from 'dompurify';

@Injectable({
  providedIn: 'root'
})
export class MarkdownService {
  private readonly sanitizer = inject(DomSanitizer);

  private escapeHtml(unsafe: string): string {
    return unsafe
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
  }

  private modifyBlockquote(value: string): string {
    const lines = value.split('\n');
  
    const modifiedLines = lines.map((line) => {
      return line.replace(/^(\s*)(>+)/, (match, leadingWhitespace, gtSequence) => {
        // Replace each '>' with '[<-%quote%->]'
        const replacements = gtSequence
          .split('')
          .map(() => '[|-%quote%-|]')
          .join('');
        return `${leadingWhitespace}${replacements}`;
      });
    });
  
    const result = modifiedLines.join('\n');
    return result;
  }

  private restoreBlockquote(value: string): string {
     value = value.replaceAll('[|-%quote%-|]', '>');
     return value;
  }


  async sanitizeValue(value: string | undefined | null): Promise<SafeHtml> {
    if(!value) {
      return '';
    }

    value = this.modifyBlockquote(value);
    value = this.escapeHtml(value);
    value = this.restoreBlockquote(value);

    const renderer = new marked.Renderer();

    renderer.codespan = ({ text, lang }: { text: string; lang?: string }) => {
      const escapedCode = DOMPurify.sanitize(text.trim());
      return `
        <code class="language-${lang ?? 'plaintext'}">${escapedCode}</code></pre>
      `;
    };

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

    const rawHtml = await marked(value);
    const cleanHtml = DOMPurify.sanitize(rawHtml);
    return this.sanitizer.bypassSecurityTrustHtml(cleanHtml);
  }
}
