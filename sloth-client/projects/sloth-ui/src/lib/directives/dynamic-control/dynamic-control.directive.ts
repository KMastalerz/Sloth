import { Directive, inject, input, ViewContainerRef } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { WebControl } from '@sloth-http';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { OpCom } from '../../op-com/op-com';

@Directive({
  selector: '[slDynamicControl]',
  standalone: true
})
export class DynamicControlDirective {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  config = input.required<WebControl>();
  index = input<number | undefined>(undefined);
  
  mainForm = input<FormGroup>();
  panelForm = input<FormGroup | FormArray>();
  opCom = input<OpCom>();


  ngOnInit(): void {  
    const component = this.directory.getControl(this.config().controlType);
    if(component) { 
      const componentRef: any = this.container.createComponent(component);
      componentRef.instance.config = this.config;
      componentRef.instance.index = this.index;
    }
  }
}
