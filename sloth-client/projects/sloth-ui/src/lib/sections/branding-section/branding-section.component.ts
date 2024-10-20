import { Component, input, signal } from '@angular/core';
import { IconComponent } from "../../icons/icon/icon.component";

@Component({
  selector: 'sl-branding-section',
  standalone: true,
  imports: [IconComponent],
  templateUrl: './branding-section.component.html',
  styleUrl: './branding-section.component.scss'
})
export class BrandingSectionComponent {
  title = signal<string>('Sloth');
  icon = signal<string>('sloth');
  showIcon = input<boolean>(true);
  showTitle = input<boolean>(true);
}
