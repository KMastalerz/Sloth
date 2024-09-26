import { Directive, inject, input, ViewContainerRef } from '@angular/core';

import { DynamicFormSync } from '../../dynamic-form-sync';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';

@Directive({
  selector: '[slDynamicSection]',
  standalone: true
})
export class DynamicSectionDirective {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  sectionID = input.required<string>();
  formSync = input.required<DynamicFormSync>();

  ngOnInit(): void {
    const config = this.formSync().getSection(this.sectionID());
    const controls = this.formSync().getSectionControls(this.sectionID());
    const component = this.directory.getSection(config.sectionType);
    
    if(component) {
      const componentRef: any = this.container.createComponent(component);
      var panelRef = this.formSync().getPanelRef(config.panelID);
      panelRef?.sections.push({
        sectionID: this.sectionID(),
        section: componentRef.instance,
        controls: []
      });
      
      componentRef.instance.config.set(config);
      componentRef.instance.formSync.set(this.formSync());
      componentRef.instance.controls.set(controls);
    }
  }
}
