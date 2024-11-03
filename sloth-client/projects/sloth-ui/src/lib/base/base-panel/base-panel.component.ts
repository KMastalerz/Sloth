import { Component, computed, inject, model } from '@angular/core';
import { WebControl, WebPanel, WebSection } from '@sloth-http';
import { JsonService } from '@sloth-shared';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  protected jsonUtil = inject(JsonService);
  pageSync = model.required<DynamicPageSync>();
  config = model.required<WebPanel>();
  gridArea = model.required<string | undefined>();
  metadata = computed<any>(() => this.jsonUtil.tryParse(this.config().metadata));
  sections = computed<WebSection[]>(() => this.config().webSections ?? []);
}