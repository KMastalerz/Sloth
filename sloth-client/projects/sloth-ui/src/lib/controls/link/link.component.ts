import { Component, computed, DestroyRef, inject } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { BaseControl } from '../../base/base-control/base-control.component';
import { LinkMetadata } from '../../models/meta-data.types';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [RouterLink, MatTooltip, MatBadge],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl {
  destroyRef = inject(DestroyRef)
  private metadata = computed<LinkMetadata | undefined | null>(() => this.jsonUtil.tryParse(this.config()?.metadata));
  private isSideNav = computed(() => this.pageSync().getWebPanelByID(this.config().panelID).panelType === 'sideNav');
  private isSideNavCollapsedState = computed(() => this.isSideNav() ? this.navCollapsed() : true);
  
  private counterSubject = computed<string | undefined>(() => this.metadata()?.counterSubject ?? undefined);
  protected warningCount = computed<number | undefined>(()=>this.metadata()?.warningCount);
  protected errorCount = computed<number | undefined>(()=>this.metadata()?.errorCount);

  protected showIcon = computed(() => !(!this.config().icon));
  protected showLabel = computed(() => !(!this.label())  && !this.isSideNavCollapsedState());
  protected showTooltip = computed(() => !(!this.tooltip()) && this.isSideNavCollapsedState());
  protected showBagde = computed(() =>  !(!this.count()) && this.isSideNavCollapsedState());


  protected count = computed<number | undefined>(() => {
    if(this.counterSubject()) {
      return this.pageSync()?.pageInstance[this.counterSubject()!]() ?? undefined;
    }
    return undefined;
  });

  protected  counterType = computed<'error' | 'warning' | 'information'>(() => {
    if(this.count() && this.errorCount() && this.count()! >= this.errorCount()!) {
      return 'error';
    } else if (this.count() && this.warningCount() && this.count()! >= this.warningCount()!) {
      return 'warning';
    } else {
      return 'information';
    }
  });
}
