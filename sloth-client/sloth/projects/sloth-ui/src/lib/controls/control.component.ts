import { Component } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule, MatTooltip } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';

import { BaseControlComponent } from './base-control.component';

@Component({
  selector: 'sl-control',
  imports: [MatTooltipModule, MatTooltip, MatBadgeModule, MatFormFieldModule],
  templateUrl: './control.component.html',
  styleUrl: './control.component.scss'
})
export class ControlComponent extends BaseControlComponent {
}
