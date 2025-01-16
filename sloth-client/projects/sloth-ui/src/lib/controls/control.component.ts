import { Component, computed, input } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'sl-control',
  imports: [MatTooltipModule, MatBadgeModule, MatFormFieldModule],
  templateUrl: './control.component.html',
  styleUrl: './control.component.scss'
})
export class ControlComponent {
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
}
