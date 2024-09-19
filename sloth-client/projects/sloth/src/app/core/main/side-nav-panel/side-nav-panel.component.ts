import { Component, computed, inject, input, signal } from '@angular/core';
import { WebControl } from '@sloth-http';

import { ControlType, NavGroup, SideNavComponent, SlothIcons, ToggleIconComponent } from '@sloth-ui';
import { ListUtilityService } from '@sloth-util';

@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [SideNavComponent, ToggleIconComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss'
})
export class SideNavPanelComponent {
  listUtil = inject(ListUtilityService);
  controls = input.required<WebControl[]>();
  logoIcon = signal<string>(SlothIcons.SlothIcon);

  protected toggleControl = computed<WebControl>(() => this.controls().find(c => c.ControlID == 'Toggle' && c.ControlType === ControlType.ToggleIcon)!);

  protected mainNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.MetaData!).Group === NavGroup.Main && c.ControlType === ControlType.SideNav));
  protected userNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.MetaData!).Group === NavGroup.User && c.ControlType === ControlType.SideNav));
  protected infoNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.MetaData!).Group === NavGroup.Info && c.ControlType === ControlType.SideNav));
  
  protected hasMainNavigation = computed<boolean>(()=> !this.listUtil.IsEmpty(this.mainNavigation()));
  protected hasUserNavigations = computed<boolean>(()=> !this.listUtil.IsEmpty(this.userNavigation()));
  protected hasInfoNavigations = computed<boolean>(()=> !this.listUtil.IsEmpty(this.infoNavigation()));

  protected collapsed = signal<boolean>(false);

  protected onToggleExpander() {
    this.collapsed.set(!this.collapsed());
  }
}
