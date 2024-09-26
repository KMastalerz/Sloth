import { Directive, inject, input, OnInit, ViewContainerRef } from '@angular/core';

import { DynamicFormSync } from '../../dynamic-form-sync';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { PanelRef } from '../../../models/page-reference.model';

@Directive({
  selector: '[slDynamicPanel]',
  standalone: true
})
export class DynamicPanelDirective implements OnInit{
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  panelID = input.required<string>();
  formSync = input.required<DynamicFormSync>();

  ngOnInit(): void {
    const config = this.formSync().getPanel(this.panelID());
    const panelForm = this.formSync().getPanelForm(this.panelID());
    const sections = this.formSync().getPanelSections(this.panelID());
    const component = this.directory.getPanel(config.panelType);
    if(component) {
      const componentRef: any = this.container.createComponent(component);
      this.formSync().pageRef.panels.push({
        panelID: this.panelID(),
        panel: componentRef.instance,
        sections: []
      } as PanelRef);
      componentRef.instance.config.set(config);
      componentRef.instance.formSync.set(this.formSync());
      componentRef.instance.sections.set(sections);
      componentRef.instance.panelForm.set(panelForm);
    }
  }
}
