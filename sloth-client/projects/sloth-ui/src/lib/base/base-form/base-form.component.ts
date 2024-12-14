import { Component, computed, inject, input, output, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { JsonService } from '@sloth-shared';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { PageLayoutMetadata } from '../../models/meta-data.types';
import { FormGroup } from '@angular/forms';
import {OpCom} from "../../op-com/op-com";

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
  mainForm = signal(new FormGroup({}));
  opCom = signal<OpCom>(new OpCom());

  config = computed(()=> this.pageSync().pageConfig);
  hasRouter = computed(()=> this.config().hasRouter);
  panels = computed(()=> this.config().webPanels);
  layout = computed<PageLayoutMetadata>(()=> this.jsonUtil.tryParse(this.config()?.layout));
  metadata = computed<any>(() => this.jsonUtil.tryParse(this.config()?.metadata));

  ngOnInit(): void {
    this.pageSync().pageInstance = this.pageInstance();

    // this.pageSync()?.toParent.pipe(untilDestroyed(this)).subscribe(action => {
    //   if(action) {
    //   }
    // });
  }
}
