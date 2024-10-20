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
  label = computed<string | undefined>(() => this.config()?.controlLabel ?? undefined);  
  placeholder = computed<string | undefined>(() => this.config()?.controlPlaceholder ?? undefined);
  tooltip = computed<string | undefined>(() => this.config()?.controlTooltip ?? undefined);
  route = computed<string | undefined>(() => this.config()?.route ?? undefined);  
  type = computed<string | undefined>(()=>this.config()?.innerType ?? undefined);
}

