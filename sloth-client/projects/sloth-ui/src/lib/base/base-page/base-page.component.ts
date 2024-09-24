import { Component, input, OnInit } from '@angular/core';
import { WebPage } from '@sloth-shared';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template: ``
})
export class BasePage implements OnInit {
  page = input.required<WebPage>();
  test = input<boolean>();
  ngOnInit(): void {
    console.log(`[BasePaPage] initialized with page ${this.page().pageID}`);
    
  }
}
