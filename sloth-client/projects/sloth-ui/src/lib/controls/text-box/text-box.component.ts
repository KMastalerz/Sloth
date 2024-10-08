import { Component, signal } from '@angular/core';
import { FormControlComponent } from '../../base/form-control/form-control.component';

@Component({
  selector: 'sl-text-box',
  standalone: true,
  imports: [],
  templateUrl: './text-box.component.html',
  styleUrl: './text-box.component.scss'
})
export class TextBoxComponent extends FormControlComponent {
  imageUrl = signal('https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/typescript/typescript-original.svg');
}
