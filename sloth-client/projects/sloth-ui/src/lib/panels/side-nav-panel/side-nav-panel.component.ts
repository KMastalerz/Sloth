import { Component, computed, inject, input, signal } from '@angular/core';

import { ListUtilityService } from '@sloth-shared';

import { BasePanel } from '../../base/base-panel/base-panel.component';
import { IPanel } from '../../base/base.interface';
import { LinkComponent } from '../../controls/link/link.component';
import { ToggleIconComponent } from '../../controls/toggle-icon/toggle-icon.component';
import { BrandingPanelComponent } from '../branding-panel/branding-panel.component';
import { SlothIcons } from '../../icons/sloth.icon';
import { NavGroup } from '../../constants/nav-group.constant';
import { ControlType } from '../../constants/controls-type.constants';


@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [LinkComponent, ToggleIconComponent, BrandingPanelComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss'
})
export class SideNavPanelComponent extends BasePanel implements IPanel {
  initPanel(): void {
    throw new Error('Method not implemented.');
  }
}
