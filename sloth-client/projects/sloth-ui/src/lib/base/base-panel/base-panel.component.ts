import { Component, computed, model } from '@angular/core';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { WebPanel } from '@sloth-http';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  pageSync = model.required<DynamicPageSync>();
  config = model.required<WebPanel>();
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));
}
