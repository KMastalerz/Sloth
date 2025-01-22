import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, computed, forwardRef, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ListItemGroup, ListSelectItem } from 'sloth-utilities';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
@Component({
  selector: 'sl-list-select',
  imports: [MatFormFieldModule, MatSelectModule, ReactiveFormsModule, MatTooltipModule, FormsModule],
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
export class ListSelectComponent extends BaseFormControlComponent {
  multiple = input<boolean>(false);
  items = input.required<any[]>();
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  hideTooltip = computed(() => !this.tooltip());

  valueKey = input<string | null | undefined>(undefined);
  groupKey = input<string | null | undefined>(undefined);
  displayKey = input<string | null | undefined>(undefined);
  hasGroups = computed<boolean>(() => this.groupKey() ? true : false);

  protected displayList = computed<ListSelectItem[]>(() => {
    const items = this.items();
    if (!items) return [];
  
    return items.map(i => ({
      value: this.valueKey() ? i[this.valueKey()!] : i,
      label: this.displayKey() ? i[this.displayKey()!] : undefined,
      group: this.groupKey() ? i[this.groupKey()!] : undefined,
    }));
  });

  protected groupedItems = computed<ListItemGroup[]>(() => {
    if(!this.hasGroups()) return [];

    const groupsMap = new Map<string, ListSelectItem[]>();
    
    this.displayList().forEach(item => {
      const groupName = item.group || 'Ungrouped';
      if (!groupsMap.has(groupName)) {
        groupsMap.set(groupName, []);
      }
      groupsMap.get(groupName)!.push(item);
    });

    // Transform the map into an array of ItemGroup objects
    return Array.from(groupsMap.entries()).map(([name, items]) => ({ name, items }));
  });
}
