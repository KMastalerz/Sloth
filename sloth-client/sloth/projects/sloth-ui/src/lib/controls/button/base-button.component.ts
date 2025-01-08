import { Component, output } from '@angular/core';
import { BaseControlComponent } from '../base-control.component';

@Component({
  selector: 'sl-base-button',
  imports: [],
  template: '',
})
export class BaseButtonComponent extends BaseControlComponent {
  onClick = output();

  public onClickEmit(): void {
    this.onClick.emit();
  }
}
