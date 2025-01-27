import { Component, computed, input, model, OnDestroy, OnInit, Optional, signal } from '@angular/core';
import { ControlContainer, ControlValueAccessor, FormArray, FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { FormMode } from '../../models/form-mode.model';

enum FormType {
  Array,
  Control, 
  Group
}
@Component({
  selector: 'sl-base-form-control',
  imports: [],
  template: ''
})
export class BaseFormControlComponent implements ControlValueAccessor, OnInit, OnDestroy {
  // Basic form mode
  formMode = input<FormMode>(FormMode.Edit);

  // Names used to look up from *parent* form
  formControlName = input<string | null | undefined>(undefined);

  // Bounded values
  formControl = model<FormControl | null | undefined>(undefined);
  formGroup = signal<FormGroup | null | undefined>(undefined);
  formArray = signal<FormArray | null | undefined>(undefined);

  // Our local "value" model, only used if we have a FormControl
  value = model<any | any[]>();

  // Quick “what type is the final control?” 
  formType = signal<FormType | null>(null);

  // For error display if we are a FormControl
  isError = computed<boolean>(() => {
    const c = this.formControl();
    return !!(c && c.invalid && (c.touched || c.dirty));
  });

  // Basic sample logic for canEdit
  isEditMode = computed<Boolean>(() => this.formMode() === FormMode.Edit);
  isRequired = computed<boolean>(()=> true);

  private valueChangeSub: Subscription | undefined = undefined;
  private onChangeFn: (value: any) => void = () => {};
  private onTouchedFn: () => void = () => {};

  constructor(@Optional() private controlContainer: ControlContainer) {}

  ngOnInit() {
    const parent = this.controlContainer?.control as FormGroup | FormArray | undefined;


    if(parent instanceof FormGroup) {
      this.formGroup.set(parent)
    }
    if(parent instanceof FormArray) {
      this.formArray.set(parent)
    }

    if(parent && this.formControlName()) {
      const control = parent.get(this.formControlName()!) as FormControl;
      this.formControl.set(control);
    }
    


    if(this.formControl()) {
      this.value.set(this.formControl()?.value)
      this.valueChangeSub = this.formControl()!.valueChanges.subscribe(val => {
        this.value.set(val);
        this.onChangeFn(val);
      });
    }
  }

  ngOnDestroy() {
    if (this.valueChangeSub) {
      this.valueChangeSub.unsubscribe();
      this.valueChangeSub = undefined;
    }
  }

  writeValue(obj: any): void {
    if(obj === this.value()) return;
    this.value.update(() => obj);
  }

  registerOnChange(fn: any): void { 
    this.value.subscribe((value) => fn(value));
  }

  registerOnTouched(fn: () => void): void {
    this.onTouchedFn = fn;
  }

  onTouched() {
    this.onTouchedFn();
  }

  setDisabledState?(isDisabled: boolean): void {
    const ctrl = this.formControl();
    if (ctrl) {
      if(isDisabled !== ctrl.disabled)
        isDisabled ? ctrl.disable() : ctrl.enable();
    }
  }
}