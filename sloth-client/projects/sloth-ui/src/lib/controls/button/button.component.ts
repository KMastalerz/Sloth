import { Component, computed } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';
@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl {
  protected onClick() {
    if(this.parent() && this.action() && this.action() in this.parent()) {
      this.parent()[this.action()]();
    }
  }
}
