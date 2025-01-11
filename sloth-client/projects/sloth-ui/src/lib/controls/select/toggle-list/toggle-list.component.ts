import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, forwardRef, input } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatError, MatHint } from '@angular/material/form-field';
import { ToggleListItem } from 'sloth-utilities';
import { BaseSelectComponent } from '../base-select.component';

@Component({
  selector: 'sl-toggle-list',
  imports: [MatButtonToggleModule, ReactiveFormsModule, FormsModule, MatError, MatHint],
  templateUrl: './toggle-list.component.html',
  styleUrl: './toggle-list.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ToggleListComponent),
      multi: true
    }
  ],
})
export class ToggleListComponent extends BaseSelectComponent {
  items = input<ToggleListItem[]>([]);
  hideIndicator = input<boolean>(true);
}
