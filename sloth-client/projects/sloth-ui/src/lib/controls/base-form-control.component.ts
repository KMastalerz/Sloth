import { Component, computed, input, model, output } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'sl-base-form-control',
  imports: [],
  template: ''
})
export class BaseFormControlComponent implements ControlValueAccessor {
  value = model<any | any[]>();
  valueChanges = output<string | number | boolean | Date | File | FileList | null | undefined>();
  name = input<string>('');
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  badge = input<number | string | null>(null);
  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());

  constructor(public ngControl: NgControl){
    if(this.ngControl) 
      this.ngControl.valueAccessor = this;    

    this.value.subscribe((value)=> {
        this.valueChanges.emit(value);
    });
  }
  
  writeValue(obj: any): void {
    this.value.update(() => obj);
  }

  registerOnChange(fn: any): void { 
    this.value.subscribe((value) => fn(value));
  }
  
  registerOnTouched(fn: any): void {}

  setDisabledState?(isDisabled: boolean): void {}

  private onChange = (value: any) => {};

  private onTouched = () => {};
}
