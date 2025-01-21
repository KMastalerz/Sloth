import { Component, computed, input } from '@angular/core';
import { BaseFormControlComponent } from '../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-base-input',
  imports: [],
  template: ''
})
export class BaseInputComponent extends BaseFormControlComponent  {
  name = input<string>('');
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
  hint = input<string>('');
}
