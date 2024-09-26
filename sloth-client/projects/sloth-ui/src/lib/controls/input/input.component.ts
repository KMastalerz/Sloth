import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { IBaseControl } from '../../engine/base/base-control.interface';

@Component({
  selector: 'sl-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './input.component.html',
  styleUrl: './input.component.scss'
})
export class InputComponent extends BaseControl implements IBaseControl {
  setMetadata(): void {
    if (this.metaData()){
      this.icon.set(this.metaData().icon ?? 'eye');
    }
  }
  
  icon = model<string | undefined>(undefined);
}

