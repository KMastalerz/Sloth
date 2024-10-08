import { Component, input } from '@angular/core';
import { DynamicFormComponent, DynamicPageSync } from '@sloth-ui';

@Component({
  selector: 'sl-project-options',
  standalone: true,
  imports: [DynamicFormComponent],
  templateUrl: './project-options.component.html',
  styleUrl: './project-options.component.scss'
})
export class ProjectOptionsComponent {
  pageSync = input.required<DynamicPageSync>();

  ngOnInit() {  
    console.log('ProjectOptionsComponent initialized', this.pageSync());
    
  }

  onAction(action: any) {
    console.log('ProjectOptionsComponent action', action);
  }
}
