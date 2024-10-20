import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormControlComponent } from '../../base/form-control/form-control.component';

@Component({
  selector: 'sl-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordComponent extends FormControlComponent{
  protected show = signal<boolean>(false);
  showHide = computed(()=> this.show() ? 'visibility_off' : 'visibility');

  protected toggleVisibility(): void {
    this.show.set(!this.show());
  }
}

