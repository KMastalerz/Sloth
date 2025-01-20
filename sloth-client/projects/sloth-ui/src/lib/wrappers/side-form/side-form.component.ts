import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'sl-side-form',
  imports: [ReactiveFormsModule],
  templateUrl: './side-form.component.html',
  styleUrl: './side-form.component.scss'
})
export class SideFormComponent {
  formGroup = input<FormGroup>(new FormGroup({}));
}
