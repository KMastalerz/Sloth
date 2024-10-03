import { Component, input } from '@angular/core';
import { DynamicFormComponent, PageSync } from '@sloth-ui';

@Component({
  selector: 'sl-project-options',
  standalone: true,
  imports: [DynamicFormComponent],
  templateUrl: './project-options.component.html',
  styleUrl: './project-options.component.scss'
})
export class ProjectOptionsComponent {
  pageSync = input.required<PageSync>();
}
