import { Component, forwardRef } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatTimepickerModule } from '@angular/material/timepicker';
import { ControlComponent } from '../../control.component';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-time-picker',
  imports: [MatInputModule, ControlComponent, FormsModule, MatTimepickerModule],
  templateUrl: './time-picker.component.html',
  styleUrl: './time-picker.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TimePickerComponent),
      multi: true
    }
  ],
})
export class TimePickerComponent extends BaseInputComponent implements ControlValueAccessor {

}
