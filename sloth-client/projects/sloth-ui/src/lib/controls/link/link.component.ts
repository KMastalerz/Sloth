import { Component, computed, input, signal } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink, RouterLinkActive } from '@angular/router';

import { BaseControl } from '../../base/base-control/base-control.component';
import { IControl } from '../../base/base.interface';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule, RouterLink, RouterLinkActive],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl implements IControl {
  setMetadata(): void {
    throw new Error('Method not implemented.');
  }
}
