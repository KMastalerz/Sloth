import { Component } from '@angular/core';
import { BaseSection } from '../../engine/base/base-section/base-section.component';
import { DynamicControlDirective } from '../../engine/directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-simple-section',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './simple-section.component.html',
  styleUrl: './simple-section.component.scss'
})
export class SimpleSectionComponent extends BaseSection {}
