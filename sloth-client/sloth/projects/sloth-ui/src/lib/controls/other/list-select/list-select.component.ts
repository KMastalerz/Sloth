import { FormsModule } from '@angular/forms';
import { Component, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { ListSelectItem } from '../../../models/list-select-item.model';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-list-select',
  imports: [MatFormFieldModule, MatSelectModule, FormsModule, ControlComponent],
  templateUrl: './list-select.component.html',
  styleUrl: './list-select.component.scss'
})
export class ListSelectComponent extends BaseFormControlComponent {
  multiple = input<boolean>(false);
  items = input<ListSelectItem[]>([]);
}
