import { Directive, inject, input, OnInit, ViewContainerRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { WebPanel } from '@sloth-http';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';
import { OpCom } from '../../op-com/op-com';

@Directive({
  selector: '[slDynamicPanel]',
  standalone: true,
})
export class DynamicPanelDirective implements OnInit {
  private container = inject(ViewContainerRef);
  private directory = inject(DynamicDirectoryService);

  config = input.required<WebPanel>();
  gridArea = input<string | undefined>(undefined);
  mainForm = input<FormGroup>();
  opCom = input<OpCom>();

  ngOnInit(): void {
    const component = this.directory.getPanel(this.config().panelType);
    if(component) { 
      const componentRef: any = this.container.createComponent(component);
      componentRef.instance.config.set(this.config());
      componentRef.instance.gridArea.set(this.gridArea() ? `grid-area: ${this.gridArea()}` : undefined);
    }
  }
}
