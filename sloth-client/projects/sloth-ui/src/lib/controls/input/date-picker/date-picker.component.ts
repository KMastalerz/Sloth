import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, forwardRef } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ControlComponent } from '../../control.component';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-date-picker',
  imports: [MatInputModule, ControlComponent, ReactiveFormsModule, FormsModule, MatDatepickerModule],
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatePickerComponent),
      multi: true
    }
  ],
})
export class DatePickerComponent extends BaseInputComponent implements ControlValueAccessor {

}
