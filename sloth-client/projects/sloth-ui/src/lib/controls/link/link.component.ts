import { Component, computed, OnInit, signal } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { BaseControl } from '../../base/base-control/base-control.component';
import { Action, ActionType } from '../../page-sync/action';
import { CollapseDirective } from '../../directives/collapse/collapse.directive';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [RouterLink, MatTooltip, MatBadge, CollapseDirective],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl implements OnInit {
  collapsed = signal<boolean>(false);

  icon = computed<string | undefined>(()=>this.metaData()?.icon ?? undefined);
  type = computed<string | undefined>(()=>this.metaData()?.type ?? undefined);
  color = computed<string | undefined>(()=>this.metaData()?.color ?? undefined);

  warningCount = computed<number | undefined>(()=>this.metaData()?.warningCount);
  errorCount = computed<number | undefined>(()=>this.metaData()?.errorCount);

  checkResult = signal<string>('info');
  count = signal<number>(0);

  ngOnInit(): void {
    //listen to collapsed update 
    this.pageSync()?.toChild.pipe(untilDestroyed(this)).subscribe(action => {
      if(action)
        this.onAction(action);
    });
  }

  private onAction(action: Action): void {
    switch (action.actionType) {
      case ActionType.CollapseLink: 
        this.collapsed.set(action.param);
        break;
    }
  }
}
