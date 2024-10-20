import { Component, input } from '@angular/core';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template:''
})
export class BasePage {
  pageSync = input.required<DynamicPageSync>();
}
