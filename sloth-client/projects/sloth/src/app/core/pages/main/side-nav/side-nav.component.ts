import { Component, computed, input, signal } from '@angular/core';
import { WebControl } from '@sloth-http';
import { MainPageSection } from '@sloth-shared';
import { BasePanel, BrandingSectionComponent, CollapseDirective, IconNames } from '@sloth-ui';
import { SideNavLinkComponent } from "./side-nav-link/side-nav-link.component";


@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [BrandingSectionComponent, CollapseDirective, SideNavLinkComponent],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent extends BasePanel {
  collapsed = signal<boolean>(false);
  icon = computed(()=> this.collapsed() ? IconNames.ChevronRight : IconNames.ChevronLeft);

  mainSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.Main));
  userSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.User));
  infoSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.Info));
  mainSectionHeader = signal(MainPageSection.Main);
  userSectionHeader = signal(MainPageSection.User);
  infoSectionHeader = signal(MainPageSection.Info);

  onCollapseToggle(): void {
    this.collapsed.set(!this.collapsed());
  }
}
