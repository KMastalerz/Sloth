import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from '../../control.component';
import { BaseNavigationComponent } from '../base-navigation.component';

@Component({
  selector: 'sl-header-link',
  imports: [MatButtonModule, ControlComponent],
  templateUrl: './header-link.component.html',
  styleUrl: './header-link.component.scss'
})
export class HeaderLinkComponent extends BaseNavigationComponent {}
