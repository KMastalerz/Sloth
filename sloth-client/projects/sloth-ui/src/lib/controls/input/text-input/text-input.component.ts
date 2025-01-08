import { Component, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-text-input',
  imports: [FormsModule, MatInputModule, ControlComponent],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent extends BaseFormControlComponent  {}
