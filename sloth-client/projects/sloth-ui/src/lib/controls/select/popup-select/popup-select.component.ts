import { Component, computed, input, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-popup-select',
  imports: [MatInputModule, ReactiveFormsModule, FormsModule, MatTooltipModule, MatIconModule, MatButtonModule],
  templateUrl: './popup-select.component.html',
  styleUrl: './popup-select.component.scss'
})
export class PopupSelectComponent extends BaseFormControlComponent {
  multiple = input<boolean>(false);
  items = input.required<any[]>();
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  popupPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());
  component = input.required<any>()

  display = signal<string>('');

  onPopupOpen(): void {
    // open in ng container new popup which is on absolute position, positioned next to control in relation based on popupPosition signal
    // its moved by 1rem from control (up, down, left or right, depending on popupPosition)
    // this popup will build a component passed in component signal, such components will have to contain input signal for items, multiple
    // when component is closed it will throw and outValue: {value: any, display: string}, which will be passed to this component and pass value as this controls value, and display as this display.
    // the component will contain logic to get items from database etc. 
  }
}
