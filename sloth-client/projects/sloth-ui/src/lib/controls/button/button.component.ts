import { Component } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';
import { IControl } from '../../base/base.interface';
@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl implements IControl {
  setMetadata(): void {
    throw new Error('Method not implemented.');
  }
}
