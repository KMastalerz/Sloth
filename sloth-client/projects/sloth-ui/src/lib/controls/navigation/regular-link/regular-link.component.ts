import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'sl-regular-link',
  imports: [RouterLink],
  templateUrl: './regular-link.component.html',
  styleUrl: './regular-link.component.scss'
})
export class RegularLinkComponent {
  label = input<string>('');
  link = input<string>('');
}
