import { Component, computed, OnInit, signal } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { BaseControl } from '../../base/base-control/base-control.component';
import { Action, ActionType } from '../../page-sync/action';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [RouterLink, MatTooltip, MatBadge],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl implements OnInit {
  collapsed = signal<boolean>(false);

  icon = computed(()=>this.metaData()?.icon ?? 'question_mark');
  type = computed(()=>this.metaData()?.type ?? 'headerNav');

  warningCount = computed<number | undefined>(()=>this.metaData()?.warningCount);
  errorCount = computed<number | undefined>(()=>this.metaData()?.errorCount);

  checkResult = signal<string>('info');
  count = signal<number>(0);

  override ngOnInit(): void {
    super.ngOnInit();

    //listen to collapsed update 
    this.pageSync()?.toChild.subscribe(action => {
      if(action)
        this.onAction(action);
    });
  }

  private onAction(action: Action): void {
    switch (action.actionType) {
      case ActionType.Collapse: 
        this.collapsed.set(action.param);
        break;
    }
  }
}
