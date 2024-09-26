import { Component } from '@angular/core';
import { BasePanel } from '../../engine/base/base-panel/base-panel.component';
import { DynamicSectionDirective } from '../../engine/directives/dynamic-section/dynamic-section.directive';

@Component({
  selector: 'sl-simple-panel',
  standalone: true,
  imports: [DynamicSectionDirective],
  templateUrl: './simple-panel.component.html',
  styleUrl: './simple-panel.component.scss'
})
export class SimplePanelComponent extends BasePanel {

}
