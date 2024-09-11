import { Component, input } from '@angular/core';

@Component({
  selector: 'sl-base-control',
  standalone: true,
  template: ``
})
export class BaseControlComponent {
  config = input.required<any>()
}
