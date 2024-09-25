import { Component } from '@angular/core';
import { BasePage, DynamicPanelDirective } from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [DynamicPanelDirective],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {}

