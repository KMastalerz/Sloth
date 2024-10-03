import { Component, computed } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';
import { WebControl, WebSection } from '@sloth-http';

@Component({
  selector: 'sl-grid-form',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './grid-form.component.html',
  styleUrl: './grid-form.component.scss'
})
export class GridFormComponent extends BasePanel {
  columns  = computed<string>(()=> {
    const count = this.metaData()?.columns ?? 1;
    return `repeat(${count}, 1fr)`;
  });

  sections = computed<WebSection[]>(()=> this.pageSync().getWebSectionsByID(this.config()?.panelID));

  protected getControlConfig(panelID: string, sectionID: string, controlID: string): WebControl {
    return this.pageSync().getWebControlByIDs(panelID, sectionID, controlID);
  }

  protected getSectionGroupedControls(section: WebSection): WebControl[] {
    if(section.metaData) {
      const metaData = JSON.parse(section.metaData);

      if(metaData.groupedControls) {
        const orderedControls = metaData.groupedControls.split(',');
        return orderedControls.map((controlID: string)=> {
          return this.getControlConfig(section.panelID, section.sectionID, controlID.trim());
        });
      }
    }
    return [];
  };

  protected getSectionNonGroupedControls(section: WebSection): WebControl[] {
    if(section.metaData) {
      const metaData = JSON.parse(section.metaData);

      if(metaData.groupedControls) {
        const orderedControls = metaData.groupedControls.split(',');
        return section.webControls?.filter(control => !orderedControls.includes(control.controlID)) ?? [];
      }
    }
    return [];

  }
}
