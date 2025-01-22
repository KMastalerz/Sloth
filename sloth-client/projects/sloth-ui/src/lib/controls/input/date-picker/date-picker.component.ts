import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, computed, forwardRef, input } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { v4 as uuidv4 } from 'uuid';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-date-picker',
  imports: [MatInputModule, ReactiveFormsModule, FormsModule, MatDatepickerModule, MatTooltipModule],
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
export class DatePickerComponent extends BaseFormControlComponent {
  name = input<string>(this.formControlName() ?? uuidv4());
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());
}
