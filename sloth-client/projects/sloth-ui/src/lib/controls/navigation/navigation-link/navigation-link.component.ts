import { Component, input } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from "../../control.component";
import { BaseNavigationComponent } from '../base-navigation.component';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'sl-navigation-link',
  imports: [MatIcon, MatButtonModule, ControlComponent, RouterLink, RouterLinkActive],
  templateUrl: './navigation-link.component.html',
  styleUrl: './navigation-link.component.scss'
})
export class NavigationLinkComponent extends BaseNavigationComponent {
  icon = input.required<string>();
  value = input<number | undefined>(undefined);
  target = input<string>('dashboard');
}
