import { Component, computed } from '@angular/core';
import { NgClass } from '@angular/common';
import { MatTooltip } from '@angular/material/tooltip';
import { MatBadge } from '@angular/material/badge';
import { BaseControl } from '../../base/base-control/base-control.component';
import { Action, ActionType } from '../../page-sync/action';
import { ButtonMetadata } from '../../models/meta-data.types';

@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [MatTooltip, MatBadge, NgClass],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl {
  private metadata = computed<ButtonMetadata | undefined | null>(() => this.jsonUtil.tryParse(this.config()?.metadata));
  private isSideNav = computed(() => this.pageSync().getWebPanelByID(this.config().panelID).panelType === 'sideNav');
  private isSideNavCollapsedState = computed(() => this.isSideNav() ? this.navCollapsed() : true);
  
  private counterSubject = computed<string | undefined>(() => this.metadata()?.counterSubject ?? undefined);
  protected warningCount = computed<number | undefined>(()=>this.metadata()?.warningCount);
  protected errorCount = computed<number | undefined>(()=>this.metadata()?.errorCount);

  protected showIcon = computed(() => !(!this.config().icon) );
  protected showLabel = computed(() => !(!this.label()) && !this.isSideNavCollapsedState());
  protected showTooltip = computed(() => !(!this.tooltip()) && this.isSideNavCollapsedState());
  protected showBagde = computed(() => this.count());

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

  onClick(): void {
    let action: Action | undefined;
    let param: any;
    switch(this.action()) {
      case 'submit':
      case 'delete':
        action = {
          actionType: this.action() === 'submit' ? ActionType.Submit : ActionType.Delete,
          param: this.pageSync()?.pageForm
        } as Action;
        break;
      case 'submitPanel':
      case 'deletePanel':
        param = this.pageSync()?.getPanelForm(this.config().panelID);
        action = {
          actionType: this.action() === 'submitPanel' ? ActionType.SubmitPanel : ActionType.DeletePanel,
          param: param.value,
          panelID: this.config().panelID
        } as Action;
        break;
      case 'submitControl':
      case 'deleteControl':
        param = this.pageSync()?.getFormControl(this.config().panelID, this.config().controlID, this.index());
        action = {
          actionType: this.action() === 'submitControl' ? ActionType.SubmitControl : ActionType.DeleteControl,
          param: param.value,
          controlID: this.config().controlID
        } as Action;
        break;
      default:
        return;
    }
    if(param?.valid) {
      this.pageSync()!.toParent.next(action);
    }
  }
}
