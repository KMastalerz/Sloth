import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, forwardRef, inject, input } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatError, MatHint } from '@angular/material/form-field';
import { NgStyle } from '@angular/common';
import { HexService, ToggleListItem } from 'sloth-utilities';
import { BaseSelectComponent } from '../base-select.component';

@Component({
  selector: 'sl-toggle-list',
  imports: [MatButtonToggleModule, ReactiveFormsModule, FormsModule, MatError, MatHint, NgStyle],
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
  private readonly hexServices = inject(HexService);
  items = input<ToggleListItem[]>([]);
  hideIndicator = input<boolean>(true);

  getColor(color: string | null): string | null {
    return this.hexServices.getAccessibleFontColor(color);
  }

  isChecked(value: any): boolean {
    return  value === this.value();
  }
}
