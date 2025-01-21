import { Component, computed, input, Optional, Self } from '@angular/core';
import { BaseFormControlComponent } from '../base-form-control/base-form-control.component';
import { ControlContainer } from '@angular/forms';

@Component({
  selector: 'sl-base-select',
  imports: [],
  template: ''
})
export class BaseSelectComponent extends BaseFormControlComponent {
  name = input<string>('');
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  hint = input<string>('');
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
}
