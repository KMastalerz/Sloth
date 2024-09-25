import { Component } from '@angular/core';
import { BasePanel } from '../../engine/base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../engine/directives/dynamic-control/dynamic-control.directive';


@Component({
  selector: 'sl-login-panel',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './login-panel.component.html',
  styleUrl: './login-panel.component.scss'
})
export class LoginPanelComponent extends BasePanel {
  
}
