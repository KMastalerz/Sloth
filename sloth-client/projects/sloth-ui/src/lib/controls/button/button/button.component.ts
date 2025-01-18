import { Component, computed, input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from '../../control.component';
import { BaseButtonComponent } from '../base-button.component';

@Component({
  selector: 'sl-button',
  imports: [MatButtonModule, ControlComponent],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseButtonComponent {
  isHeader = input<boolean>(false);
  displayType = input<'add' | 'delete' | 'flat' | 'basic'>('basic');
  isFlatButton = computed(()=> this.displayType() !== 'basic');
}
