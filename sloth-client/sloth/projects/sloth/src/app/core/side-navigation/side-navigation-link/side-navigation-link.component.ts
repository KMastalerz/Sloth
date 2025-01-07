import { Component, input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'side-navigation-link',
  imports: [MatIcon, MatButtonModule],
  templateUrl: './side-navigation-link.component.html',
  styleUrl: './side-navigation-link.component.scss'
})
export class SideNavigationLinkComponent {
  icon = input.required<string>();
  label = input.required<string>();
  value = input<number | undefined>(undefined);
}
