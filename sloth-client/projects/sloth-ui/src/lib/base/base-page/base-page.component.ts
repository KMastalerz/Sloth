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
  hasRouter = computed(()=> this.config().hasRouter);
  panels = computed(()=> this.config().webPanels);
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));
  actionEvent = output<Action>();

  ngOnInit(): void {
    this.pageSync()?.toParent.pipe(untilDestroyed(this)).subscribe(action => {
      if(action) {
        this.actionEvent.emit(action);
      }
    });
  }
}
