import { Component, signal } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-text-input',
  standalone: true,
  imports: [],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent extends BaseControl {
  constructor(){
    super();
    this.prompts.set(this.controlPrompts);
  }

  public prompts = signal<any>(undefined);

  private controlPrompts: any = {
    required: 'Required',
  }
}
