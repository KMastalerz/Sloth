import { Component, computed, inject, input, output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { JsonService } from '@sloth-shared';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { Action } from '../../page-sync/action';

@UntilDestroy()
@Component({
  selector: 'sl-base-form',
  standalone: true,
  template:''
})
export class BaseForm {
  protected router = inject(Router);
  protected activatedRoute = inject(ActivatedRoute);
  private jsonUtil = inject(JsonService);
  pageSync = input.required<DynamicPageSync>();
  pageInstance = input.required<any>();
  
  config = computed(()=> this.pageSync().pageConfig);
  hasRouter = computed(()=> this.config().hasRouter);
  panels = computed(()=> this.config().webPanels);
  metaData = computed<any>(() => this.jsonUtil.tryParse(this.config()?.metaData));
  actionEvent = output<Action>();

  ngOnInit(): void {
    this.pageSync().pageInstance = this.pageInstance();

    this.pageSync()?.toParent.pipe(untilDestroyed(this)).subscribe(action => {
      if(action) {
        this.actionEvent.emit(action);
      }
    });
  }
}
