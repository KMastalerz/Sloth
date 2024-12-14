import { Component } from '@angular/core';
import { BasePage, DynamicFormComponent } from '@sloth-ui';

@Component({
  selector: 'sl-test',
  standalone: true,
  imports: [DynamicFormComponent],
  templateUrl: './test.component.html',
  styleUrl: './test.component.scss'
})
export class TestComponent extends BasePage {
  
}
