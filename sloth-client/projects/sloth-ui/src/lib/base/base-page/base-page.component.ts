import { Component, computed, inject, input } from '@angular/core';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template:''
})
export class BasePage {
  protected router = inject(Router);
  protected activatedRoute = inject(ActivatedRoute);
  pageSync = input.required<DynamicPageSync>();
  config = computed(()=> this.pageSync().pageConfig);
  panels = computed(()=> this.config().webPanels);
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));

  orientation = computed<string>(() => this.metaData()?.orientation ?? 'vertical')
  position = computed<string>(() => this.metaData()?.position ?? 'top')
  background = computed<string>(() => this.metaData()?.background ?? 'secondary')
}
