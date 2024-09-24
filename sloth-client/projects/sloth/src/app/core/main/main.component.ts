import { Component } from '@angular/core';
import { BasePage, SideNavPanelComponent } from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavPanelComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {}

