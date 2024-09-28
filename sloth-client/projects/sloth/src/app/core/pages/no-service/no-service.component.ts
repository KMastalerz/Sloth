import { Component, computed, signal } from '@angular/core';
import { BrandingSectionComponent } from '@sloth-ui';

@Component({
  selector: 'sl-no-service',
  standalone: true,
  imports: [BrandingSectionComponent],
  templateUrl: './no-service.component.html',
  styleUrl: './no-service.component.scss'
})
export class NoServiceComponent {
}
