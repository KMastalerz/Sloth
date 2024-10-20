import { Directive, inject, input, ViewContainerRef } from '@angular/core';
import { WebControl } from '@sloth-http';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Directive({
  selector: '[slDynamicControl]',
  standalone: true
})
export class DynamicControlDirective {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  pageSync = input.required<DynamicPageSync>();
  config = input.required<WebControl>();
  index = input<number | undefined>(undefined);
  navCollapsed = input<boolean>(false);

  ngOnInit(): void {  
    const component = this.directory.getControl(this.config().controlType);
    if(component) { 
      const componentRef: any = this.container.createComponent(component);
      componentRef.instance.config = this.config;
      componentRef.instance.pageSync = this.pageSync;
      componentRef.instance.index = this.index;
      if(this.navCollapsed() !== undefined && this.navCollapsed() !== null)
        componentRef.instance.navCollapsed = this.navCollapsed;
    }
  }
}
