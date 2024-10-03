import { Directive, inject, input, OnInit, ViewContainerRef } from '@angular/core';
import { WebPanel } from '@sloth-http';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Directive({
  selector: '[slDynamicPanel]',
  standalone: true,
})
export class DynamicPanelDirective implements OnInit {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  pageSync = input.required<DynamicPageSync>();
  config = input.required<WebPanel>();

  ngOnInit(): void {
    const component = this.directory.getPanel(this.config().panelType);
    if(component) { 
      const componentRef: any = this.container.createComponent(component);
      componentRef.instance.config.set(this.config());
      componentRef.instance.pageSync.set(this.pageSync());
    }
  }
}
