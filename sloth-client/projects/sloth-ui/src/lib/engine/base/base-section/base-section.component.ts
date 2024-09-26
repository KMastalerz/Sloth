import { Component, computed, model, signal } from '@angular/core';

import { WebControl, WebSection } from '@sloth-http';

import { DynamicFormSync } from '../../dynamic-form-sync';

@Component({
  selector: 'sl-base-section',
  standalone: true,
  template: ``

})
export class BaseSection {
  config = model.required<WebSection>();
  formSync = model.required<DynamicFormSync>();
  controls = model.required<WebControl[]>();
  label = computed(() => this.config().sectionLabel);

  collapsed = signal<boolean>(false);
}
