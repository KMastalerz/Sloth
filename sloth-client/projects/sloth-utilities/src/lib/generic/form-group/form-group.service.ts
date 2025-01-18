import { inject, Injectable } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormGroupService {
  private readonly formBuilder = inject(FormBuilder);

  createFormGroup(obj: any): FormGroup {
    if(!obj) return new FormGroup({});
    const group: { [key: string]: any } = {};

    Object.keys(obj).forEach(key => {
      group[key] = new FormControl(obj[key]);
    });
  
    return this.formBuilder.group(group);
  }
}
