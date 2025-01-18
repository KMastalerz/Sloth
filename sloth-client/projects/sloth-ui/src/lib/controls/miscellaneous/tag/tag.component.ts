import { NgStyle } from '@angular/common';
import { Component, computed, inject, input } from '@angular/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { HexService } from 'sloth-utilities';

@Component({
  selector: 'sl-tag',
  imports: [NgStyle, MatTooltipModule],
  templateUrl: './tag.component.html',
  styleUrl: './tag.component.scss'
})
export class TagComponent {
  private readonly herService = inject(HexService);
  tag = input<string | null | undefined>(undefined);
  tooltip = input<string | null | undefined>(undefined);
  hideTooltip = computed<boolean>(()=> this.tooltip() === '');
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  backgroundColor = input<string | null | undefined>(null);
  color = computed(()=> this.herService.getAccessibleFontColor(this.backgroundColor()) ?? null);
}
