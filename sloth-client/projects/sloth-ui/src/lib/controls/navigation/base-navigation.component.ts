import { Component, computed, input, model } from '@angular/core';

@Component({
  selector: 'sl-base-navigation',
  imports: [],
  template: ''
})
export class BaseNavigationComponent {
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  isDisabled = model<boolean>(false);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
}
