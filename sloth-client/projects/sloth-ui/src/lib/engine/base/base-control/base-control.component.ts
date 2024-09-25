import { Component, computed, input, model, OnInit, output, signal } from '@angular/core';
import { ControlValueAccessor, FormControl } from '@angular/forms';
import { UntilDestroy } from '@ngneat/until-destroy';

import { WebControl } from '@sloth-http';
import { DynamicFormSync } from '../../dynamic-form-sync';

@UntilDestroy({ checkProperties: true })
@Component({
  standalone: true,
  template: ''
})
export class BaseControl implements ControlValueAccessor, OnInit {
  class = model.required<string>();
  config = model.required<WebControl>();
  formSync = model.required<DynamicFormSync>();
  formControl = model.required<FormControl>();
  //specific properties for any contols
  metaData = model<any>();
  //actionEvent = propagate output signal
  actionEvent = output<any>();

  //base properties of WebControl
  action = computed(()=>this.config().action);
  label = computed(()=>this.config().controlLabel);
  placeholder = computed(()=>this.config().controlPlaceholder);

  value = model<string | number | boolean | Date | null | undefined>();
  
  ngOnInit(): void {
    this.value.subscribe(value => {
      this.writeValue(value);
    });
  }

  writeValue(obj: any): void {
    this.formControl().setValue(obj);  // Make sure this is setting a valid primitive
  }

  registerOnChange(fn: any): void {
    this.formControl().valueChanges.subscribe(fn);
  }
  
  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {}
}


