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
  //temp 
  count = signal<number | null>(null);
  type = computed<string>(() => {
    if (this.count() === null) {
      return 'primary';
    } else if (this.count() === 0) {
      return 'warn';
    } else {
      return 'accent';
    }
  });
}
