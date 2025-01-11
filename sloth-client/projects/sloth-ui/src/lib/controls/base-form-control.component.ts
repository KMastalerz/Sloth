import { Component, input, model, OnDestroy, OnInit, Optional, signal } from '@angular/core';
import { ControlContainer, ControlValueAccessor, FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'sl-base-form-control',
  imports: [],
  template: ''
})
export class BaseFormControlComponent implements ControlValueAccessor, OnInit, OnDestroy {
  isFormControl = false;
  parentForm = signal<FormGroup | undefined>(undefined);
  formControl = signal<FormControl | undefined>(undefined);
  isError = signal<boolean>(false);
  formControlName = input<string | undefined>(undefined);
  value = model<any | any[]>();

  constructor(@Optional() private controlContainer: ControlContainer) {}

  private valueChangeSub: Subscription | undefined = undefined;
  ngOnInit() {
    if (this.controlContainer) {
      this.parentForm.set(<FormGroup>this.controlContainer.control);
      if (this.parentForm()) {
        this.isFormControl = true;
        if (this.formControlName()) { 
          this.formControl.set(this.parentForm()!.get(this.formControlName()!) as FormControl);
          this.valueChangeSub = this.formControl()?.valueChanges.subscribe(() => {
            if (this.formControl()?.invalid && this.formControl()?.touched) {
              this.isError.set(true);
            }
          })
        }
      }
    }
  }

  ngOnDestroy() {
    if(this.valueChangeSub) {
      this.valueChangeSub.unsubscribe();
      this.valueChangeSub = undefined;
    }
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
