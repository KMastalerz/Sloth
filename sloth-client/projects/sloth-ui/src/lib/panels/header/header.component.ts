import { Component } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-header',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent extends BasePanel {}
