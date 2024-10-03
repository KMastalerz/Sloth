import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BasePage, DynamicRouterFormComponent } from '@sloth-ui';

@Component({
  selector: 'sl-project-management',
  standalone: true,
  imports: [RouterOutlet, DynamicRouterFormComponent],
  templateUrl: './project-management.component.html',
  styleUrl: './project-management.component.scss'
})
export class ProjectManagementComponent extends BasePage {}
