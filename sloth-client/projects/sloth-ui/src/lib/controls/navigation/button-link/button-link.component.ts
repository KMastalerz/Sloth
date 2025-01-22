import { Component, computed, input } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
@Component({
  selector: 'sl-button-link',
  imports: [MatButtonModule, MatTooltipModule, MatBadgeModule],
  templateUrl: './button-link.component.html',
  styleUrl: './button-link.component.scss'
})
export class ButtonLinkComponent {
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  isDisabled = input<boolean>(false);
  hideBadge = computed(() => !this.badge());
  hideTooltip = computed(() => !this.tooltip());
}
