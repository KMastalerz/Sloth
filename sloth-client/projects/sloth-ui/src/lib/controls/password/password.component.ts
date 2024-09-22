import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordComponent extends BaseControl {
  value = model<any>();
}
