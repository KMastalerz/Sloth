import { Component, computed, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-password-input',
  imports: [FormsModule, MatInputModule, ControlComponent, MatIconModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.scss'
})
export class PasswordInputComponent extends BaseFormControlComponent {
  show = signal<boolean>(false);
  showHide = computed(()=> this.show() ? 'visibility_off' : 'visibility')
  controlType  = computed(()=> this.show() ? 'text': 'password')
  toggleVisibility(): void {
    this.show.set(!this.show());
  }
}
