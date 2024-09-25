import { Component, signal } from '@angular/core';

import { BasePanel } from '../../engine/base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../engine/directives/dynamic-control/dynamic-control.directive';
import { BrandingPanelComponent } from '../branding-panel/branding-panel.component';

@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [DynamicControlDirective, BrandingPanelComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss'
})
export class SideNavPanelComponent extends BasePanel {
  collapsed = signal<boolean>(false);

  onToggle() {
    this.collapsed.set(!this.collapsed());    
  }
}
