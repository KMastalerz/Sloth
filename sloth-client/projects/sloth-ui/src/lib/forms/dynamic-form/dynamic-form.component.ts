import { Component } from '@angular/core';
import { DynamicPanelDirective } from '../../directives/dynamic-panel/dynamic-panel.directive';
import { BaseForm } from '../../base/base-form/base-form.component';

@Component({
  selector: 'sl-dynamic-form',
  standalone: true,
  imports: [DynamicPanelDirective],
  templateUrl: './dynamic-form.component.html',
  styleUrl: './dynamic-form.component.scss'
})
export class DynamicFormComponent extends BaseForm {}
