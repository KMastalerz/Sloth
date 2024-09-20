import { Component, computed, inject, input, signal } from '@angular/core';
import { WebControl } from '@sloth-http';

import { BrandingComponent, ControlType, NavGroup, SideNavComponent, SlothIcons, ToggleIconComponent } from '@sloth-ui';
import { ListUtilityService } from '@sloth-util';

@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [SideNavComponent, ToggleIconComponent, BrandingComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss'
})
export class SideNavPanelComponent {
  listUtil = inject(ListUtilityService);
  controls = input.required<WebControl[]>();
  logoIcon = signal<string>(SlothIcons.SlothIcon);

  public mainNavGroup = signal<string>(NavGroup.Main);
  public userNavGroup = signal<string>(NavGroup.User);
  public infoNavGroup = signal<string>(NavGroup.Info);

  protected toggleControl = computed<WebControl>(() => this.controls().find(c => c.controlID == 'Toggle' && c.controlType === ControlType.ToggleIcon)!);

  protected mainNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.metaData!).group === NavGroup.Main && c.controlType === ControlType.SideNav));
  protected userNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.metaData!).group === NavGroup.User && c.controlType === ControlType.SideNav));
  protected infoNavigation = computed<WebControl[]>(() => this.controls().filter(c => JSON.parse(c.metaData!).group === NavGroup.Info && c.controlType === ControlType.SideNav));
  
  protected hasMainNavigation = computed<boolean>(()=> !this.listUtil.IsEmpty(this.mainNavigation()));
  protected hasUserNavigations = computed<boolean>(()=> !this.listUtil.IsEmpty(this.userNavigation()));
  protected hasInfoNavigations = computed<boolean>(()=> !this.listUtil.IsEmpty(this.infoNavigation()));

  protected collapsed = signal<boolean>(false);

  protected onToggleExpander() {
    this.collapsed.set(!this.collapsed());
  }
}
