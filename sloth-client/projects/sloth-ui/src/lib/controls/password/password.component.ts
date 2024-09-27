import { Component, computed, signal } from '@angular/core';
import { BaseControl } from '../../base/base-control/base-control.component';
import { IconNames } from '../../constants/icon.constants';

@Component({
  selector: 'sl-password',
  standalone: true,
  imports: [],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordComponent extends BaseControl{
  protected show = signal<boolean>(false);
  protected type = computed(() => this.show()? 'text' : 'password');
  icon = computed(()=>this.metaData()?.icon ?? '');
  showHide = computed(()=> this.show() ? IconNames.Hide : IconNames.Show);

  protected toggleVisibility(): void {
    this.show.set(!this.show());
  }
}

