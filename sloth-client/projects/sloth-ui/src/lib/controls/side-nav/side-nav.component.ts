import { Component, computed, input, signal } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';

import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent extends BaseControl {
  collapsed = input.required<boolean>();
  icon = computed<string>(() => this.metaData().Icon);
  label = computed<string>(() => this.config().ControlLabel!);
  tooltip = computed<string>(() => this.config().ControlTooltip!);
  //temp 
  count = signal<number>(0);
  type = signal<string>('info');
  
}
