import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { WebPage } from '@sloth-http';

@Injectable({
  providedIn: 'root'
})
export class FormBuilderService {
  private formBuilder = inject(FormBuilder);
  buildForm(page: WebPage): FormGroup | undefined {
    if (!page) {
      throw console.error('[FormBuilderService] No page provided');
    }


    let fb = this.formBuilder.group({});





    return undefined;
  }
}
