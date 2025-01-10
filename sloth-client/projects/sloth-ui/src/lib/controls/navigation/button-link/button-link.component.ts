import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { BaseNavigationComponent } from '../base-navigation.component';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-button-link',
  imports: [MatButtonModule, ControlComponent],
  templateUrl: './button-link.component.html',
  styleUrl: './button-link.component.scss'
})
export class ButtonLinkComponent extends BaseNavigationComponent {

}
