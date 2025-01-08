import { Component, input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ControlComponent } from "../../control.component";
import { BaseButtonComponent } from '../base-button.component';

@Component({
  selector: 'sl-flat-button',
  imports: [MatButtonModule, ControlComponent],
  templateUrl: './flat-button.component.html',
  styleUrl: './flat-button.component.scss'
})
export class FlatButtonComponent extends BaseButtonComponent {
  isHeader = input<boolean>(false);
  type = input<'add' | 'delete' | null>(null);
}
