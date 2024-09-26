import { Component, input } from '@angular/core';
import { PageSync } from '../../page/page-sync';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  pageSync = input.required<PageSync>();
}
