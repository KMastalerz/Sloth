import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'sl-branding',
  standalone: true,
  imports: [],
  templateUrl: './branding-panel.component.html',
  styleUrl: './branding-panel.component.scss'
})
export class BrandingPanelComponent {
  title = input.required<string>();
  imgPath = input.required<string>();
  showTitle = input.required<boolean>();
  imgAlt = computed(() => `${this.title} logo`);
}
