import { Component, computed, inject, input, model } from '@angular/core';
import { WebControl } from '@sloth-http';
import { JsonService } from '@sloth-shared';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Component({
  selector: 'sl-base-control',
  standalone: true,
  template:''
})
export class BaseControl  {
  protected jsonUtil = inject(JsonService);
  navCollapsed = input<boolean>(false);
  config = input.required<WebControl>();
  pageSync = input.required<DynamicPageSync>();
  index = input.required<number | undefined>();

  controlID = computed<string>(() => this.config()?.controlID ?? '');
  validation = computed<any>(() => this.jsonUtil.tryParse(this.config()?.validation));

  action = computed<string | undefined>(() => this.config()?.action ?? undefined);
  icon = computed<string | undefined>(()=>this.config()?.icon ?? undefined);
  innerType = computed<string | undefined>(() => this.config()?.innerType ?? undefined);
  label = computed<string | undefined>(() => this.config()?.label ?? undefined);  
  placeholder = computed<string | undefined>(() => this.config()?.placeholder ?? undefined);
  tooltip = computed<string | undefined>(() => this.config()?.tooltip ?? undefined);
  tooltipPosition = computed<'above' | 'below' | 'left' | 'right'>(() => this.config()?.tooltipPosition ?? 'right');
  route = computed<string | undefined>(() => this.config()?.route ?? undefined);  
  size = computed<string | undefined>(() => this.config()?.size ?? undefined);
  style = computed<string | undefined>(() => this.config()?.style ?? undefined);
  type = computed<string | undefined>(()=>this.config()?.innerType ?? undefined);
}

