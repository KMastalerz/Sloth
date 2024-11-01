import { Component, computed, signal } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { BrandingSectionComponent } from '../../sections/branding-section/branding-section.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [BrandingSectionComponent, DynamicControlDirective],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent extends BasePanel {
  collapsed = signal<boolean>(true);
  toggleIcon = computed<string>(()=>this.collapsed() ? 'chevron_left' : 'chevron_right');
  navSections = computed(()=>this.sections().filter(s => s.position !== 'bottom'));
  bottomSections = computed(()=>this.sections().filter(s => s.position === 'bottom'));
  onClick() {
    this.collapsed.set(!this.collapsed());
  }
}
