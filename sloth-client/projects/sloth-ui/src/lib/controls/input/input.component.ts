import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BaseControl } from '../../base/base-control/base-control.component';
import { IControl } from '../../base/base.interface';

@Component({
  selector: 'sl-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent extends BaseControl implements IControl {
  setMetadata(): void {
    throw new Error('Method not implemented.');
  }
}

