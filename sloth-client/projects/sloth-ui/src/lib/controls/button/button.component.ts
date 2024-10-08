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
      case 'submitSection':
      case 'deleteSection':
        param = this.pageSync()?.getSectionForm(this.config().panelID, this.config().sectionID, this.index());
        action = {
          actionType: this.action() === 'submitSection' ? ActionType.SubmitSection : ActionType.DeleteSection,
          param: param.value,
          sectionID: this.config().sectionID
        } as Action;
        break;
      case 'submitControl':
      case 'deleteControl':
        param = this.pageSync()?.getFormControl(this.config().panelID, this.config().sectionID, this.config().controlID, this.index());
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
