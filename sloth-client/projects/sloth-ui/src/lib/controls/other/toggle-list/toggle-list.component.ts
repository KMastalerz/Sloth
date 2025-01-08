import { FormsModule } from '@angular/forms';
import { Component, input } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { BaseFormControlComponent } from '../../base-form-control.component'; 
import { ToggleListItem } from '../../../models/toggle-list-item.model';

@Component({
  selector: 'sl-toggle-list',
  imports: [MatButtonToggleModule, FormsModule],
  templateUrl: './toggle-list.component.html',
  styleUrl: './toggle-list.component.scss'
})
export class ToggleListComponent extends BaseFormControlComponent {
  items = input<ToggleListItem[]>([]);
  hideIndicator = input<boolean>(true);
}
