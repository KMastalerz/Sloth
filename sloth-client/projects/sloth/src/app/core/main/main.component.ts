import { Component, model } from '@angular/core';
import { SideNavConfig } from '@sloth-ui';
import { SideNavPanelComponent } from "./side-nav-panel/side-nav-panel.component";

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavPanelComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {
  mainNavigations = model<SideNavConfig[]>([])
  userNavigations = model<SideNavConfig[]>([])
  settingsNavigations = model<SideNavConfig[]>([])
}
