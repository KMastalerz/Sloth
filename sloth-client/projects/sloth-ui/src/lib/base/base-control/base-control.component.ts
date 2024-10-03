import { Component, computed, input, model } from '@angular/core';
import { WebControl } from '@sloth-http';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Component({
  selector: 'sl-base-control',
  standalone: true,
  template:''
})
export class BaseControl  {
  config = model.required<WebControl>();
  pageSync = model.required<DynamicPageSync>();

  controlID = computed<string>(() => this.config()?.controlID ?? '');
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));
  validation = computed<any>(() => JSON.parse(this.config()?.validation ?? '{}'));

  action = computed<string>(() => this.config()?.action ?? '');
  tooltip = computed<string>(() => this.config()?.controlTooltip ?? '');  
  label = computed<string>(() => this.config()?.controlLabel ?? '');  
  placeholder = computed<string>(() => this.config()?.controlPlaceholder ?? '');
  route = computed<string>(() => this.config()?.route ?? '');  
}
