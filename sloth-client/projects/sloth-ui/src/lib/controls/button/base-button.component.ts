import { Component, computed, input, model, output } from '@angular/core';

@Component({
  selector: 'sl-base-button',
  imports: [],
  template: '',
})
export class BaseButtonComponent {
  onClick = output();
  type = input<'menu' | 'button' | 'submit' | 'reset'>('button');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  isDisabled = model<boolean>(false);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());

  public onClickEmit(): void {
    this.onClick.emit();
  }
}
