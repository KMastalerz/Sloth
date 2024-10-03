import { Component, computed } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';
import { Action, ActionType } from '../../page-sync/action';
@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl {
  icon = computed(()=>this.metaData()?.icon ?? '');

  onClick(): void {
    let action: Action | undefined;

    switch(this.action()) {
      case 'submit':
        action = {
          actionType: ActionType.Submit,
          param: this.pageSync()?.pageForm
        } as Action;
        break;
      case 'submitPanel':
        action = {
          actionType: ActionType.SubmitPanel,
          param: this.pageSync()?.getPanelFormByID(this.config()?.panelID!)
        } as Action;
        break;
      case 'submitControl':
          action = {
            actionType: ActionType.SubmitControl,
            // param: this.pageSync()?.getControlByIDs(this.config()?.panelID!)
          } as Action;
          break;
      default:
        return;
    }
    this.pageSync()!.toParent.next(action);
  }
}
