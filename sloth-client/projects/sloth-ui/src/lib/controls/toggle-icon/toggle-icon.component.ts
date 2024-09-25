import { Component, computed, OnInit } from '@angular/core';

import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { NgClass } from '@angular/common';


@Component({
  selector: 'sl-toggle-icon',
  standalone: true,
  imports: [NgClass],
  templateUrl: './toggle-icon.component.html',
  styleUrl: './toggle-icon.component.scss'
})
export class ToggleIconComponent extends BaseControl implements OnInit {
  toggleIcon = computed<string>(() => this.value() ? this.metaData().onFalse : this.metaData().onTrue);
  size = computed<string>(() => this.metaData().size);

  onToggle() {
    this.value.set(!this.value());
    this.actionEvent.emit(this.value());
  }
}
