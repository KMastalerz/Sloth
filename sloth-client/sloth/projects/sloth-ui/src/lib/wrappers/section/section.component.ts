import { Component, input } from '@angular/core';

@Component({
  selector: 'sl-section',
  imports: [],
  templateUrl: './section.component.html',
  styleUrl: './section.component.scss'
})
export class SectionComponent {
  header = input<string>();
}
