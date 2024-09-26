import { Component, computed, input, signal } from '@angular/core';


@Component({
  selector: 'sl-branding-section',
  standalone: true,
  imports: [],
  templateUrl: './branding-section.component.html',
  styleUrl: './branding-section.component.scss'
})
export class BrandingSectionComponent {
  title = signal<string>('Sloth');
  imgPath = signal<string>('assets/icon.png');
  imgAlt = computed(() => `${this.title} logo`);
  collapsed = input<boolean>(false);
}
