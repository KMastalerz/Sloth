import { Component, computed, input, signal } from '@angular/core';
import { WebControl } from '@sloth-http';
import { MainPageSection } from '@sloth-shared';
import { ActionType, BasePanel, BrandingSectionComponent, CollapseDirective, IconNames, LinkComponent } from '@sloth-ui';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [BrandingSectionComponent, CollapseDirective, LinkComponent],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent extends BasePanel {
  collapsed = signal<boolean>(false);
  icon = computed(()=> this.collapsed() ? IconNames.ChevronRight : IconNames.ChevronLeft);

  mainSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.Main));
  userSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.User));
  infoSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.Info));
  adminSection = computed<WebControl[]>(() => this.pageSync().getControlBySectionID(MainPageSection.Admin));

  mainSectionHeader = signal(MainPageSection.Main);
  userSectionHeader = signal(MainPageSection.User);
  infoSectionHeader = signal(MainPageSection.Info);
  adminSectionHeader = signal(MainPageSection.Admin);

  onCollapseToggle(): void {
    this.collapsed.set(!this.collapsed());
    this.pageSync().toChild.next({ actionType: ActionType.Collapse, param: this.collapsed() });
  }
}
