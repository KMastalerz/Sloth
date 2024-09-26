import { Directive, inject, input, ViewContainerRef } from '@angular/core';

import { DynamicFormSync } from '../../dynamic-form-sync';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';

@Directive({
  selector: '[slDynamicControl]',
  standalone: true
})
export class DynamicControlDirective {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  controlID = input.required<string>();
  formSync = input.required<DynamicFormSync>();
  value = input<string | number | boolean | Date | null | undefined>(undefined);

  ngOnInit(): void {
    const config = this.formSync().getControl(this.controlID());
    const formControl = this.formSync().getControlForm(this.controlID());
    const component = this.directory.getControl(config.controlType);

    if(component) {
      const componentRef: any = this.container.createComponent(component);
      componentRef.instance.config.set(config);
      componentRef.instance.formSync.set(this.formSync());
      componentRef.instance.formControl.set(formControl);
      componentRef.instance.value.set(this.value());
      componentRef.instance.setConfig();
      var sectionRef = this.formSync().getSectionRef(config.panelID, config.sectionID);
      sectionRef?.controls.push({
        controlID: this.controlID(),
        control: componentRef.instance
      });

      if(config.metaData)
        componentRef.instance.metaData.set(JSON.parse(config.metaData));

      componentRef.instance.setMetadata();
    }
  }
}
