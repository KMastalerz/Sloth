import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { IBaseControl } from '../../engine/base/base-control.interface';

@Component({
  selector: 'sl-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordComponent extends BaseControl implements IBaseControl {
  setMetadata(): void {
    if (this.metaData()){
      this.icon.set(this.metaData().icon ?? 'eye');
    }
  }
  
  icon = model<string | undefined>(undefined);
}

