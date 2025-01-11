import { Component, computed, forwardRef, signal } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from '../../control.component';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-password-input',
  imports: [FormsModule, MatInputModule, ControlComponent, MatIconModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => PasswordInputComponent),
      multi: true
    }
  ],
})
export class PasswordInputComponent extends BaseInputComponent implements ControlValueAccessor {
  show = signal<boolean>(false);
  showHide = computed(()=> this.show() ? 'visibility_off' : 'visibility')
  controlType  = computed(()=> this.show() ? 'text': 'password')
  toggleVisibility(): void {
    this.show.set(!this.show());
  }
}
