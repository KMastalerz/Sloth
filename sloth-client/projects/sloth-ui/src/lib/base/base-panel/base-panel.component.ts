import { Component, computed, inject, input, model } from '@angular/core';
import { WebPanel, WebSection } from '@sloth-http';
import { JsonService } from '@sloth-shared';
import { OpCom } from '../../op-com/op-com';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  protected jsonUtil = inject(JsonService);
  config = model.required<WebPanel>();
  gridArea = model.required<string | undefined>();
  metadata = computed<any>(() => this.jsonUtil.tryParse(this.config().metadata));
  opCom = input<OpCom>();
  sections = computed<WebSection[]>(() => this.config().webSections ?? []);
}