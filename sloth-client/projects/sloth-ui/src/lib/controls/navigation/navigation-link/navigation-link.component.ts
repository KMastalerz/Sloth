import { Component, input } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from "../../control.component";
import { BaseNavigationComponent } from '../base-navigation.component';

@Component({
  selector: 'sl-navigation-link',
  imports: [MatIcon, MatButtonModule, ControlComponent],
  templateUrl: './navigation-link.component.html',
  styleUrl: './navigation-link.component.scss'
})
export class NavigationLinkComponent extends BaseNavigationComponent {
  icon = input.required<string>();
  value = input<number | undefined>(undefined);
}
