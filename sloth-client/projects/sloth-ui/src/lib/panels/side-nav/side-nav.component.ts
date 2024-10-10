import { Component, signal } from '@angular/core';
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
  collapsed = signal<boolean>(false);
}
