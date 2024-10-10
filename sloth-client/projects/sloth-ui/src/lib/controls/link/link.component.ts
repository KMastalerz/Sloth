import { Component, computed, OnInit, signal } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { BaseControl } from '../../base/base-control/base-control.component';
import { Action, ActionType } from '../../page-sync/action';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [RouterLink, MatTooltip, MatBadge],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl implements OnInit {
  // TO DO: Promably can be passed as input from parent.
  collapsed = signal<boolean>(false);
  
  color = computed<string | undefined>(()=>this.metaData()?.color ?? 'transparent');

  warningCount = computed<number | undefined>(()=>this.metaData()?.warningCount);
  errorCount = computed<number | undefined>(()=>this.metaData()?.errorCount);

  // TO DO: Implement, on error fallback to default
  checkResult = signal<string>('info');
  count = signal<number>(0);


  // TO DO: If can be passed as input from parent, than remove below.
  ngOnInit(): void {
    console.log('LinkComponent ngOnInit', this.config());
    
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
