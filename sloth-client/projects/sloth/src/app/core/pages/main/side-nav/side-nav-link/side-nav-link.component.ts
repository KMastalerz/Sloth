import { Component, computed, input, ViewEncapsulation } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BaseControl } from '@sloth-ui';

@Component({
  selector: 'sl-side-nav-link',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule, RouterLink, RouterLinkActive],
  templateUrl: './side-nav-link.component.html',
  styleUrl: './side-nav-link.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class SideNavLinkComponent extends BaseControl {
  collapsed = input.required<boolean>();

  icon = computed(()=>this.metaData()?.icon ?? '');
  type = input<string>('info');
  count = input<number>(10);
}
