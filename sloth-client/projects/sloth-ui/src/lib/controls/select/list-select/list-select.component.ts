import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, forwardRef, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ListSelectItem } from 'sloth-utilities';
import { ControlComponent } from '../../control.component';
import { BaseSelectComponent } from '../base-select.component';

@Component({
  selector: 'sl-list-select',
  imports: [MatFormFieldModule, MatSelectModule, ReactiveFormsModule, ControlComponent, FormsModule],
  templateUrl: './list-select.component.html',
  styleUrl: './list-select.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ListSelectComponent),
      multi: true
    }
  ],
})
export class ListSelectComponent extends BaseSelectComponent {
  multiple = input<boolean>(false);
  items = input<ListSelectItem[]>([]);
}
