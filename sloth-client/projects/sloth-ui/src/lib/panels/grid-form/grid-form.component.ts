import { Component, computed } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';
import { WebControl, WebSection } from '@sloth-http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'sl-grid-form',
  standalone: true,
  imports: [DynamicControlDirective, FormsModule],
  templateUrl: './grid-form.component.html',
  styleUrl: './grid-form.component.scss'
})
export class GridFormComponent extends BasePanel {
  columns  = computed<string>(()=> {
    const count = this.metaData()?.columns ?? 1;
    return `repeat(${count}, minmax(calc(100% / ${count} - 1rem), 1fr))`
  });
  
  protected getSectionGroupedControls(section: WebSection): string[] | undefined {
    if(section.metaData) {
      const metaData = this.jsonUtil.tryParse(section.metaData);

      if(metaData.groupedControls) {
        return metaData.groupedControls;
      }
    }
    return undefined;
  };

  protected getControl(section: WebSection, controlID: string): WebControl {
    return section.webControls?.find(c => c.controlID === controlID.trim()) as WebControl;
  }
}
