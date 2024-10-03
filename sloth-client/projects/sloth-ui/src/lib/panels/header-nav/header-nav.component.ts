import { Component } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-header-nav',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './header-nav.component.html',
  styleUrl: './header-nav.component.scss'
})
export class HeaderNavComponent extends BasePanel {}
