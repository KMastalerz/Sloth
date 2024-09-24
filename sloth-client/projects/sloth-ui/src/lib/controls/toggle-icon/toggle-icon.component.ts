import { Component, computed, output, signal } from '@angular/core';

import { BaseControl } from '../../base/base-control/base-control.component';
import { IControl } from '../../base/base.interface';


@Component({
  selector: 'sl-toggle-icon',
  standalone: true,
  imports: [],
  templateUrl: './toggle-icon.component.html',
  styleUrl: './toggle-icon.component.scss'
})
export class ToggleIconComponent extends BaseControl implements IControl {
  setMetadata(): void {
    throw new Error('Method not implemented.');
  }
}
