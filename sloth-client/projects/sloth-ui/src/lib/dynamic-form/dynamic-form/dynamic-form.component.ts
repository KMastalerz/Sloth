import { Component, input, output } from '@angular/core';
import { PageSync } from '@sloth-ui';
import { Action } from '../../page-sync/action';

@Component({
  selector: 'sl-dynamic-form',
  standalone: true,
  imports: [],
  templateUrl: './dynamic-form.component.html',
  styleUrl: './dynamic-form.component.scss'
})
export class DynamicFormComponent {
  pageSync = input.required<PageSync>();
  action = output<Action>();
}
