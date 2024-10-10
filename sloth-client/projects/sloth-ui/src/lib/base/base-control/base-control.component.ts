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
  index = model.required<number | undefined>();

  controlID = computed<string>(() => this.config()?.controlID ?? '');
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));
  validation = computed<any>(() => JSON.parse(this.config()?.validation ?? '{}'));

  action = computed<string | undefined>(() => this.config()?.action ?? undefined);
  icon = computed<string | undefined>(()=>this.config()?.icon ?? undefined);
  label = computed<string | undefined>(() => this.config()?.controlLabel ?? undefined);  
  placeholder = computed<string | undefined>(() => this.config()?.controlPlaceholder ?? undefined);
  route = computed<string | undefined>(() => this.config()?.route ?? undefined);  
  tooltip = computed<string | undefined>(() => this.config()?.controlTooltip ?? undefined);  
  type = computed<string | undefined>(()=>this.config()?.internalType ?? undefined);
}

