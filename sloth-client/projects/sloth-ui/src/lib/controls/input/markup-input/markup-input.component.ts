import { Component, forwardRef, input } from '@angular/core';
import { FormGroup, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ControlComponent } from '../../control.component';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-markup-input',
  imports: [ReactiveFormsModule, MatInputModule, ControlComponent, FormsModule],
  templateUrl: './markup-input.component.html',
  styleUrl: './markup-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MarkupInputComponent),
      multi: true
    }
  ],
})
export class MarkupInputComponent extends BaseInputComponent {}
