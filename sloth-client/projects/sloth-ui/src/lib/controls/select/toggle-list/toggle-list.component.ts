import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, computed, forwardRef, inject, input } from '@angular/core';
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
  items = input<any[]>([]);
  hideIndicator = input<boolean>(true);
  valueKey = input<string | null | undefined>(undefined);
  backgroundKey = input<string | null | undefined>(undefined);
  displayKey = input<string | null | undefined>(undefined);

  protected displayList = computed<ToggleListItem[]>(() => {
    const items = this.items();
    if (!items) return [];
  
    return items.map(i => ({
      value: this.valueKey() ? i[this.valueKey()!] : i,
      label: this.displayKey() ? i[this.displayKey()!] : undefined,
      color: this.backgroundKey() ? i[this.backgroundKey()!] : undefined,
    }));
  });
  
  getColor(color: string | null): string | null {
    return this.hexServices.getAccessibleFontColor(color);
  }

  isChecked(value: any): boolean {
    return  value === this.value();
  }
}
