import { Component, computed, input, signal } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink, RouterLinkActive } from '@angular/router';

import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule, RouterLink, RouterLinkActive],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl {
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
