import { Component, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormControlComponent } from '../../base/form-control/form-control.component';

@Component({
  selector: 'sl-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent extends FormControlComponent {
}

