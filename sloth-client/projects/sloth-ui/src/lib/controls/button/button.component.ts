import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import { MatTooltip } from '@angular/material/tooltip';
import { MatBadge } from '@angular/material/badge';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [MatTooltip, MatBadge, NgClass],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl {
 
}
