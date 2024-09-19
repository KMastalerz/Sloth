import { Component, computed, input } from '@angular/core';
import { WebControl, WebPage } from '@sloth-http';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template: ``
})
export class BasePage {
  page = input.required<WebPage>();
  controls = computed<WebControl[]>(()=> this.page().WebControls ?? []);
}
