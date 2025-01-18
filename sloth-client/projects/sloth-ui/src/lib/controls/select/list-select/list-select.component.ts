import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, computed, forwardRef, input } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ListItemGroup, ListSelectItem } from 'sloth-utilities';
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
  hasGroups = computed(() => this.items().some(item=> !!item.group) ?? false)
  groupedItems = computed<ListItemGroup[]>(() => {
    if(!this.hasGroups()) return [];

    const groupsMap = new Map<string, ListSelectItem[]>();
    
    this.items().forEach(item => {
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
