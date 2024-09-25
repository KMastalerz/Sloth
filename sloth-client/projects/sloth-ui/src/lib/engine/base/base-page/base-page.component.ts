import { Component, computed, input, OnInit } from '@angular/core';
import { WebPage } from '@sloth-http';
import { FormGroup } from '@angular/forms';
import { DynamicFormSync } from '../../dynamic-form-sync';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template: ``
})
export class BasePage implements OnInit {
  formSync = input.required<DynamicFormSync>();
  pageConfig = computed<WebPage>(() => this.formSync().pageConfig);
  pageForm = computed<FormGroup>(() => this.formSync().pageForm);
  ngOnInit(): void {
    this.formSync().pageRef = this;
  }
}
