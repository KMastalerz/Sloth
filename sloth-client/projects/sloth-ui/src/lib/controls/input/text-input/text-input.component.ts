import { Component, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ControlComponent } from '../../control.component';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-text-input',
  imports: [MatInputModule, ControlComponent, ReactiveFormsModule, FormsModule],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextInputComponent),
      multi: true
    }
  ],
})
export class TextInputComponent extends BaseInputComponent implements ControlValueAccessor {}
