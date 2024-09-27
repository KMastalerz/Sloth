import { Component, computed } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';
@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl {
  icon = computed(()=>this.metaData()?.icon ?? '');
}
