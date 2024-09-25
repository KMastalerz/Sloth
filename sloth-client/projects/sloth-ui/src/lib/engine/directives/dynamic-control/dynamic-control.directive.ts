import { Directive, inject, input, output, ViewContainerRef } from '@angular/core';

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
  class = input<string>('');
  value = input<string | number | boolean | Date | null | undefined>(undefined);

  actionEvent = output<any>();

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
      componentRef.instance.class.set(this.class());
      if(config.metaData)
        componentRef.instance.metaData.set(JSON.parse(config.metaData)); 

      if (componentRef.instance.actionEvent) {
        componentRef.instance.actionEvent.subscribe((eventData: any) => {
          // Emit the event from this directive
          this.actionEvent.emit(eventData);
        });
      }
    }
  }
}
