import { Component, computed } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-text-area',
  standalone: true,
  imports: [],
  templateUrl: './text-area.component.html',
  styleUrl: './text-area.component.scss'
})
export class TextAreaComponent extends BaseControl {
  isRequired = computed(()=>this.metaData()?.required ?? true);
}
