import { Component, input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template: ``
})
export class BasePanel {
  pageForm = input<FormGroup | undefined>(undefined);
  pageReference = input<any>(undefined);
  config = input<any>(undefined);
}
