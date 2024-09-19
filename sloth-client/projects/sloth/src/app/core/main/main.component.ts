import { Component, computed } from '@angular/core';
import { SideNavPanelComponent } from "./side-nav-panel/side-nav-panel.component";
import { BasePage, SideNavModel } from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavPanelComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {}
