import { Component, model} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { WebPanel } from '@sloth-http';
import { DynamicFormSync } from '../../dynamic-form-sync';
@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template: ``
})
export class BasePanel {
  config = model.required<WebPanel>();
  formSync = model.required<DynamicFormSync>();
  panelForm = model.required<FormGroup>();
}
