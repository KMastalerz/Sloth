import { Component, computed, inject, input, output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { Action } from '../../page-sync/action';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
  actionEvent = output<Action>();

  orientation = computed<string>(() => this.metaData()?.orientation ?? 'vertical')
  position = computed<string>(() => this.metaData()?.position ?? '')
  background = computed<string>(() => this.metaData()?.background ?? 'secondary')

  ngOnInit(): void {
    console.log('[BasePage] ngOnInit', this.position());
    
    this.pageSync()?.toParent.pipe(untilDestroyed(this)).subscribe(action => {
      console.log('[BasePage] Action received', action);
      if(action) {
        this.actionEvent.emit(action);
      }
    });
  }
}
