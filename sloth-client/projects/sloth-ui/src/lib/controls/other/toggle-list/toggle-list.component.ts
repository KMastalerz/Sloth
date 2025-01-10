import { ReactiveFormsModule } from '@angular/forms';
import { Component, input } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ToggleListItem } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control.component';

@Component({
  selector: 'sl-toggle-list',
  imports: [MatButtonToggleModule, ReactiveFormsModule],
  templateUrl: './toggle-list.component.html',
  styleUrl: './toggle-list.component.scss'
})
export class ToggleListComponent extends BaseFormControlComponent {
  items = input<ToggleListItem[]>([]);
  hideIndicator = input<boolean>(true);
}
