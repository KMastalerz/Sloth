import { Component } from '@angular/core';
import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { IBaseControl } from '../../engine/base/base-control.interface';

@Component({
  selector: 'sl-button',
  standalone: true,
  imports: [],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent extends BaseControl implements IBaseControl {
  setMetadata(): void {
    
  }
  async onClick(): Promise<void> {
    // if(this.action()) { // Check if action is defined
    //   if(this.formSync()?.pageRef[this.action()!]){ // Check if action exists on the page
    //     await this.formSync()?.pageRef[this.action()!](); // Call the action
    //   }
    // }
  }
}
