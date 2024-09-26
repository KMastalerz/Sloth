import { Component, model, signal } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';

import { WebPanel, WebSection } from '@sloth-http';

import { DynamicFormSync } from '../../dynamic-form-sync';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template: ``
})
export class BasePanel {
  config = model.required<WebPanel>();
  formSync = model.required<DynamicFormSync>();
  sections = model.required<WebSection[]>();
  panelForm = model.required<FormGroup | FormArray>();

  collapsed = signal<boolean>(false);
}
