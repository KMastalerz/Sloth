import { Component, model } from '@angular/core';

import { BasePanel } from '../../engine/base/base-panel/base-panel.component';
import { DynamicSectionDirective } from '../../engine/directives/dynamic-section/dynamic-section.directive';
import { ToggleIconComponent } from "../../controls/toggle-icon/toggle-icon.component";

@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [DynamicSectionDirective, ToggleIconComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss'
})
export class SideNavPanelComponent extends BasePanel {
  onToggle() {
    this.collapsed.set(!this.collapsed());

    const panelRef = this.formSync().getPanelRef(this.config().panelID);

    if(panelRef) {
      panelRef.sections.forEach(sec => {
        const sectionRef =  sec.section;
        sectionRef.collapsed.set(this.collapsed());
        sec.controls.forEach(ctrl => {
          const controlRef = ctrl.control;
          if(controlRef.collapsed) {
            controlRef.collapsed.set(this.collapsed());
          }
        });
      });
    }
  }
}
