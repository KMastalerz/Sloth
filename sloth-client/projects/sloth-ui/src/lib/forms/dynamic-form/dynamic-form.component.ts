import { Component } from '@angular/core';
import { DynamicPanelDirective } from '../../directives/dynamic-panel/dynamic-panel.directive';
import { BasePage } from '../../base/base-page/base-page.component';

@Component({
  selector: 'sl-dynamic-form',
  standalone: true,
  imports: [DynamicPanelDirective],
  templateUrl: './dynamic-form.component.html',
  styleUrl: './dynamic-form.component.scss'
})
export class DynamicFormComponent extends BasePage {}
