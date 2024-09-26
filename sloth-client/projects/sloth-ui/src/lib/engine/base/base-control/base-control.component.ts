import { Component, model, OnInit, signal } from '@angular/core';
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
  //base properties of WebControl, passed from DynamicControlDirective
  config = model<WebControl>();
  formSync = model<DynamicFormSync>();
  formControl = model<FormControl>();
  metaData = model<any>();

  collapsed = signal<boolean>(false);
  
  //base properties of WebControl
  action = model<string | undefined>(undefined);
  class = model<string | undefined>(undefined);
  label = model<string | undefined>(undefined);
  placeholder = model<string | undefined>(undefined);
  route = model<string | undefined>(undefined);
  tooltip = model<string | undefined>(undefined);

  value = model<string | number | boolean | Date | null | undefined>();
  
  ngOnInit(): void {
    this.value.subscribe(value => {
      this.writeValue(value);
    });
  }

  setConfig(): void {
    if(this.config()) {
      this.action.set(this.config()!.action ?? undefined);
      this.class.set(this.config()!.class ?? undefined);
      this.label.set(this.config()!.controlLabel ?? undefined);
      this.placeholder.set(this.config()!.controlPlaceholder ?? undefined);
      this.route.set(this.config()!.route ?? undefined);
      this.tooltip.set(this.config()!.controlTooltip ?? undefined);
    }
  }

  writeValue(obj: any): void {
    this.formControl()?.setValue(obj);  // Make sure this is setting a valid primitive
  }

  registerOnChange(fn: any): void {
    this.formControl()?.valueChanges.subscribe(fn);
  }
  
  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {}
}


