import { Component, computed, model, output } from '@angular/core';
import { NgClass } from '@angular/common';

import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { IBaseControl } from '../../engine/base/base-control.interface';
import { IconNames } from '../../constants/icon.constants';
import { Size } from '../../constants/size.constants';


@Component({
  selector: 'sl-toggle-icon',
  standalone: true,
  imports: [NgClass],
  templateUrl: './toggle-icon.component.html',
  styleUrl: './toggle-icon.component.scss'
})
export class ToggleIconComponent extends BaseControl implements IBaseControl {
  setMetadata(): void {
    if (this.metaData()){
      this.onFalse.set(this.metaData().onFalse ?? IconNames.ChevronRight);
      this.onTrue.set(this.metaData().onTrue ?? IconNames.ChevronLeft);
      this.size.set(this.metaData().size ?? Size.Small);
    }    
  }

  toggleIcon = computed<string>(() => this.value() ? this.onTrue() : this.onFalse());

  onFalse = model<string>(IconNames.ChevronRight);
  onTrue = model<string>(IconNames.ChevronLeft);
  size = model<string>(Size.Small);

  onToggle = output<boolean | undefined>();

  onClick() {
    this.value.set(!this.value());
    this.onToggle.emit(this.value() as boolean);
  }
}
