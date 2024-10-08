import { Component, computed, signal } from '@angular/core';
import { CollapseDirective } from '../../directives/collapse/collapse.directive';
import { IconNames } from '../../constants/icon.constants';
import { ActionType } from '../../page-sync/action';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { BrandingSectionComponent } from '../../sections/branding-section/branding-section.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [CollapseDirective, BrandingSectionComponent, DynamicControlDirective],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent extends BasePanel {
  collapsed = signal<boolean>(false);
  icon = computed(()=> this.collapsed() ? IconNames.ChevronRight : IconNames.ChevronLeft);

  protected onCollapseToggle(): void {
    this.collapsed.set(!this.collapsed());
    this.pageSync().toChild.next({ actionType: ActionType.CollapseLink, param: this.collapsed() });
  }
}
