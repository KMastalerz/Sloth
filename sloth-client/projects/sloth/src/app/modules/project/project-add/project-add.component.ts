import { Component } from '@angular/core';
import { BasePage, DynamicFormComponent} from '@sloth-ui';

@Component({
  selector: 'sl-project-add',
  standalone: true,
  imports: [DynamicFormComponent],
  templateUrl: './project-add.component.html',
  styleUrl: './project-add.component.scss'
})
export class ProjectAddComponent extends BasePage {}
