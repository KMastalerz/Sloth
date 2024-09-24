import { Directive, inject, input, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';

@Directive({
  selector: '[slDynamicPanel]',
  standalone: true
})
export class DynamicPanelDirective implements OnInit{
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  pageForm = input<FormGroup | undefined>(undefined);
  pageReference = input<any>(undefined);
  config = input<any>(undefined);

  ngOnInit(): void {
    if(!this.config().type) {
      console.error('[DynamicPanelDirective] No type provided');
      return;
    }

    const component = this.directory.getPanel(this.config().type);
    if(!component) {
      console.error(`[DynamicPanelDirective] Panel ${this.config().type} not found`);
      return;
    }

    const componentRef: any = this.container.createComponent(component);
    try {
      componentRef.instance.config = this.config;
      componentRef.instance.pageForm = this.pageForm;
      componentRef.instance.pageReference = this.pageReference;
      componentRef.instance.initPanel();
    }
    catch {
      console.error(`[DynamicPanelDirective] Panel ${this.config().type} does not have all required fields, make sure it's extended by BasePanelComponent and implements IPanel`);
    }

  }
}
