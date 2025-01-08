import { Component, input, model, OnInit } from '@angular/core';
import { BaseControlComponent } from './base-control.component';
import { ControlValueAccessor, FormControl } from '@angular/forms';

@Component({
  selector: 'sl-base-form-control',
  imports: [],
  template: ''
})
export class BaseFormControlComponent extends BaseControlComponent implements ControlValueAccessor {
  value = model<string | number | boolean | Date | null | undefined>();
  name = input<string>('');
  placeholder = input<string>('');
  
  formControl: FormControl | undefined;

  writeValue(obj: any): void {
    this.formControl?.setValue(obj);  // Make sure this is setting a valid primitive
  }

  registerOnChange(fn: any): void {
    this.formControl?.valueChanges.subscribe(fn);
  }
  
  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {}
}
