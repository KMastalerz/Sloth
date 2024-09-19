import { Component, computed, output, signal } from '@angular/core';

import { BaseControl } from '../../base/base-control/base-control.component';


@Component({
  selector: 'sl-toggle-icon',
  standalone: true,
  imports: [],
  templateUrl: './toggle-icon.component.html',
  styleUrl: './toggle-icon.component.scss'
})
export class ToggleIconComponent extends BaseControl {
  onClick = output<boolean>();
  value = signal<boolean>(true);

  trueIcon = computed<string>(() => this.metaData().OnTrue);
  falseIcon = computed<string>(() => this.metaData().OnFalse);
  size = computed<string>(() => this.metaData().Size);
  icon = computed<string>(() => this.value() ? this.trueIcon() : this.falseIcon());

  protected onToggle() {
    this.value.set(!this.value());
    this.onClick.emit(this.value());
  }
}
