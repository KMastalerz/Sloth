import { Component, computed } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';
import { WebControl, WebSection } from '@sloth-http';

@Component({
  selector: 'sl-control-list',
  standalone: true,
  imports: [DynamicControlDirective],
  templateUrl: './control-list.component.html',
  styleUrl: './control-list.component.scss'
})
export class ControlListComponent extends BasePanel {
  // List panels allow only one section
  section = computed(()=> this.pageSync()?.getWebSectionsByID(this.config().panelID)[0]);

  protected getSectionGroupedControls(): string[] | undefined {
    if(this.section().metaData) {
      const metaData = JSON.parse(this.section().metaData!);

      if(metaData.groupedControls) {
        return metaData.groupedControls;
      }
    }
    return undefined;
  };

  protected getControl(controlID: string): WebControl {
    return this.section().webControls?.find(c => c.controlID === controlID.trim()) as WebControl;
  }
}
