import { Component, computed, forwardRef, input } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { v4 as uuidv4 } from 'uuid';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-text-input',
  imports: [MatInputModule, ReactiveFormsModule, FormsModule, MatTooltipModule],
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
export class TextInputComponent extends BaseFormControlComponent {
  name = input<string>(this.formControlName() ?? uuidv4());
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());
}
