import { Component, computed, forwardRef, input } from '@angular/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';;
import { MatTooltipModule } from '@angular/material/tooltip';
import { v4 as uuidv4 } from 'uuid';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-checkbox',
  imports: [MatCheckboxModule, FormsModule, MatTooltipModule],
  templateUrl: './checkbox.component.html',
  styleUrl: './checkbox.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CheckboxComponent),
      multi: true
    }
  ],
})
export class CheckboxComponent extends BaseFormControlComponent {
  name = input<string>(this.formControlName() ?? uuidv4());
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());
}
