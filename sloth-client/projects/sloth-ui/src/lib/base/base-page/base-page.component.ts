import { Component, input, OnInit } from '@angular/core';
import { PageSync } from '../../page/page-sync';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template:''
})
export class BasePage implements OnInit {
  pageSync = input.required<PageSync>();

  ngOnInit(): void {
    this.pageSync().pageRef = this;
  }
}
