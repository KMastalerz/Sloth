import { Component, input } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BaseControlComponent } from '../../base-control.component';
import { ControlComponent } from "../../control.component";

@Component({
  selector: 'sl-navigation-link',
  imports: [MatIcon, MatButtonModule, ControlComponent],
  templateUrl: './navigation-link.component.html',
  styleUrl: './navigation-link.component.scss'
})
export class NavigationLinkComponent extends BaseControlComponent {
  icon = input.required<string>();
  value = input<number | undefined>(undefined);
}
