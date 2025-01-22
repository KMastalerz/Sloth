import { Component, computed, input } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'sl-header-link',
  imports: [MatButtonModule, MatTooltipModule, MatBadgeModule],
  templateUrl: './header-link.component.html',
  styleUrl: './header-link.component.scss'
})
export class HeaderLinkComponent {
  link = input<string | null>(null);
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  isDisabled = input<boolean>(false);
  badge = input<number | string | null>(null);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
}
