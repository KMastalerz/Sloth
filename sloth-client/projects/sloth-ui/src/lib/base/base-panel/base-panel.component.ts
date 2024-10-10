import { Component, computed, model } from '@angular/core';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { WebControl, WebPanel, WebSection } from '@sloth-http';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  pageSync = model.required<DynamicPageSync>();
  config = model.required<WebPanel>();
  metaData = computed<any>(() => JSON.parse(this.config().metaData ?? '{}'));
  sections = computed<WebSection[]>(() => this.config().webSections ?? []);
  controls = computed<WebControl[]>(() => this.config().webControls ?? []);
}
