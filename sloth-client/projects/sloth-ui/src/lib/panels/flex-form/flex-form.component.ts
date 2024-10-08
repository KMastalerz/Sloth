import { Component } from '@angular/core';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';
import { WebControl, WebSection } from '@sloth-http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'sl-flex-form',
  standalone: true,
  imports: [DynamicControlDirective, FormsModule],
  templateUrl: './flex-form.component.html',
  styleUrl: './flex-form.component.scss'
})
export class FlexFormComponent extends BasePanel {
  protected getSectionGroupedControls(section: WebSection): string[] | undefined {
    if(section.metaData) {
      const metaData = JSON.parse(section.metaData);

      if(metaData.groupedControls) {
        return metaData.groupedControls;
      }
    }
    return undefined;
  };

  protected getControl(section: WebSection, controlID: string): WebControl {
    return section.webControls?.find(c => c.controlID === controlID) as WebControl;
  }
}
