import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { ControlComponent } from '../../control.component';
import { BaseFormControlComponent } from '../../base-form-control.component';

@Component({
  selector: 'sl-markup-input',
  imports: [ReactiveFormsModule, MatInputModule, ControlComponent],
  templateUrl: './markup-input.component.html',
  styleUrl: './markup-input.component.scss'
})
export class MarkupInputComponent extends BaseFormControlComponent {}
