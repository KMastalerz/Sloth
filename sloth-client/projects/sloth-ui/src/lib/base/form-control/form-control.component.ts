import { Component, model, OnInit } from '@angular/core';
import { ControlValueAccessor, FormControl } from '@angular/forms';
import { BaseControl } from '../base-control/base-control.component';

@Component({
  selector: 'sl-form-control',
  standalone: true,
  template: '',
})
export class FormControlComponent extends BaseControl implements ControlValueAccessor, OnInit {
  value = model<string | number | boolean | Date | null | undefined>();
  formControl: FormControl | undefined;

  writeValue(obj: any): void {
    this.formControl?.setValue(obj);  // Make sure this is setting a valid primitive
  }

  registerOnChange(fn: any): void {
    this.formControl?.valueChanges.subscribe(fn);
  }
  
  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {}

  ngOnInit(): void {
    const formControl = this.pageSync()?.getFormControl(this.config().panelID, this.config().controlID, this.index());
    if(formControl instanceof FormControl) 
      this.formControl = formControl

    this.value.subscribe(value => {
      this.writeValue(value);
    });
  }
}
