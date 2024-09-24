import { Directive, inject, input, ViewContainerRef } from '@angular/core';

import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { FormArray, FormGroup } from '@angular/forms';

@Directive({
  selector: '[slDynamicControl]',
  standalone: true
})
export class DynamicControlDirective {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  pageForm = input<FormGroup | undefined>(undefined);
  panelForm = input<FormGroup | FormArray | undefined>(undefined);
  panelSectionForm = input<FormGroup | FormArray | undefined>(undefined);
  pageReference = input<any>(undefined);
  panelReference = input<any>(undefined);
  config = input<any>(undefined);

  ngOnInit(): void {
    if(!this.config().type) {
      console.error('[DynamicControlDirective] No type provided');
      return;
    }

    const component = this.directory.getControl(this.config().type);
    if(!component) {
      console.error(`[DynamicControlDirective] Control ${this.config().type} not found`);
      return;
    }

    const componentRef: any = this.container.createComponent(component);
    try {
      componentRef.instance.config = this.config;
      componentRef.instance.pageForm = this.pageForm;
      componentRef.instance.panelForm = this.panelForm;
      componentRef.instance.panelSectionForm = this.panelSectionForm;
      componentRef.instance.pageReference = this.pageReference;
      componentRef.instance.panelReference = this.panelReference;
      componentRef.instance.setMetadata();
    }
    catch {
      console.error(`[DynamicControlDirective] Control ${this.config().type} does not have all required fields, make sure it's extended by BaseControlComponent and implements IControl`);
    }
  }
}
