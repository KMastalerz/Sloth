import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'sl-base-control',
  imports: [],
  template:''
})
export class BaseControlComponent {
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);

  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
}
