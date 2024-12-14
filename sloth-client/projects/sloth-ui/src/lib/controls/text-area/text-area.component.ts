import { Component, signal } from '@angular/core';
import { MatTooltip } from '@angular/material/tooltip';
import { FormControlComponent } from '../../base/form-control/form-control.component';

@Component({
  selector: 'sl-text-area',
  standalone: true,
  imports: [MatTooltip],
  templateUrl: './text-area.component.html',
  styleUrl: './text-area.component.scss'
})
export class TextAreaComponent extends FormControlComponent {
  constructor(){
    super();
    this.prompts.set(this.controlPrompts);
  }

  public prompts = signal<any>(undefined);

  private controlPrompts: any = {
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
    addImage: 'Add image'
  }
}
