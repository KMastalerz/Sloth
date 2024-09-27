import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { WebPage } from '@sloth-http';

@Injectable({
  providedIn: 'root'
})
export class FormBuilderService {
  private formBuilder = inject(FormBuilder);
  buildForm(page: WebPage): FormGroup {
    const formGroup = this.formBuilder.group({});

    return formGroup;
  }

  buildPanels() {

  }
}
