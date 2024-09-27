import { Component, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent extends BaseControl {

  icon = computed(()=>this.metaData()?.icon ?? '');
  
}

