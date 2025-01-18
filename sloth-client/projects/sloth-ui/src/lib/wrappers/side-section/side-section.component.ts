import { Component, input } from '@angular/core';

@Component({
  selector: 'sl-side-section',
  imports: [],
  templateUrl: './side-section.component.html',
  styleUrl: './side-section.component.scss'
})
export class SideSectionComponent {
  header = input<string | undefined>(undefined)
}
