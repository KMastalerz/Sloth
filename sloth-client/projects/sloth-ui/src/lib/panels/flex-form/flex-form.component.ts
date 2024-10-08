import { Component } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';
import { WebControl } from '@sloth-http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'sl-flex-form',
  standalone: true,
  imports: [DynamicControlDirective, FormsModule],
  templateUrl: './flex-form.component.html',
  styleUrl: './flex-form.component.scss'
})
export class FlexFormComponent extends BasePanel {

}
