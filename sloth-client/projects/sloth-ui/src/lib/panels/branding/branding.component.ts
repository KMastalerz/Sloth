import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'sl-branding',
  standalone: true,
  imports: [],
  templateUrl: './branding.component.html',
  styleUrl: './branding.component.scss'
})
export class BrandingComponent {
  title = input.required<string>();
  imgPath = input.required<string>();
  shoTitle = input.required<boolean>();
  imgAlt = computed(() => `${this.title} logo`);

}
