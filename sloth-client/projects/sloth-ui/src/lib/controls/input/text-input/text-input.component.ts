import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-text-input',
  imports: [MatInputModule, ControlComponent, ReactiveFormsModule],
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.scss'
})
export class TextInputComponent extends BaseFormControlComponent {
}
