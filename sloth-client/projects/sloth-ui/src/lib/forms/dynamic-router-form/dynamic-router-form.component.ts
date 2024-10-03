import { Component, computed } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BasePage } from '../../base/base-page/base-page.component';
import { DynamicPanelDirective } from '../../directives/dynamic-panel/dynamic-panel.directive';

@Component({
  selector: 'sl-dynamic-router-form',
  standalone: true,
  imports: [RouterOutlet, DynamicPanelDirective],
  templateUrl: './dynamic-router-form.component.html',
  styleUrl: './dynamic-router-form.component.scss'
})
export class DynamicRouterFormComponent extends BasePage {} 