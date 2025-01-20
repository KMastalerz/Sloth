import { Component, computed, input, model, OnDestroy, OnInit, Optional, signal } from '@angular/core';
import { AbstractControl, ControlContainer, ControlValueAccessor, FormArray, FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { FormMode } from '../models/form-mode.model';
import { P } from '@angular/cdk/keycodes';

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
  formMode = input<FormMode>(FormMode.Add);

  // Names used to look up from *parent* form
  formControlName = input<string | null | undefined>(undefined);
  formGroupName = input<string | null | undefined>(undefined);
  formArrayName = input<string | null | undefined>(undefined);

  // Bounded values
  formControl = model<FormControl | null | undefined>(undefined);
  formGroup = model<FormGroup | null | undefined>(undefined);
  formArray = model<FormArray | null | undefined>(undefined);

  formArrayControls = computed(()=> this.formArray()?.controls as FormGroup[] ?? []);
  formGroupControls = computed(()=> this.formGroup()?.controls);

  // Base control of any type => FormGroup | FormArray | FormControl 
  baseControl = model<AbstractControl | null | undefined>(undefined);

  // Our local "value" model, only used if we have a FormControl
  value = model<any | any[]>();

  // Quick “what type is the final control?” 
  formType = signal<FormType | null>(null);

  // For error display if we are a FormControl
  isError = computed<boolean>(() => {
    const c = this.baseControl();
    return !!(c && c.invalid && (c.touched || c.dirty));
  });

  // Basic sample logic for canEdit
  canEdit = computed<Boolean>(() => {
    switch (this.formMode()) {
      case FormMode.Add: 
        return true;
      default: 
        return false;
    }
  });

  private valueChangeSub: Subscription | undefined = undefined;
  private onChangeFn: (value: any) => void = () => {};
  private onTouchedFn: () => void = () => {};

  constructor(@Optional() private controlContainer: ControlContainer) {}

  ngOnInit() {
    const parent = this.controlContainer?.control as FormGroup | FormArray | undefined;

    if(this.formControlName() === 'status' ){
      console.log('parent for status', parent);
    }
    let baseControl: AbstractControl | null = null;
    if(parent) {
      // NEW START
      if(parent instanceof FormArray) {
        this.formArray.set(parent);
      }

      if(parent instanceof FormGroup){
        this.formGroup.set(parent)
      }

      

      // NEW END


      if(this.formControlName()) {        
        const control = parent?.get(this.formControlName()!);
        if(control instanceof FormControl) {
          this.formControl.set(control as FormControl);
          baseControl = control;
        }
        else  {
          this.formControl.set(null);
          baseControl = null;
        }
      }
      else if(this.formGroupName()) {
        const group = parent;

        if(group instanceof FormGroup) {
          this.formGroup.set(group as FormGroup);
          baseControl = group;
        }
        else  {
          this.formControl.set(null);
          baseControl = null;
        }
      }
      else if(this.formArrayName()) {
        const array = parent;

        if(array instanceof FormArray) {
          this.formArray.set(array as FormArray);
          baseControl = array;
        }
        else  {
          this.formControl.set(null);
          baseControl = null;
        }
      }
      else {
        this.formControl.set(null);
      }
    }

    this.baseControl.set(baseControl);

    if(this.baseControl()) {
      this.valueChangeSub = this.baseControl()!.valueChanges.subscribe(val => {
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
    const ctrl = this.baseControl();
    if (ctrl) {
      isDisabled ? ctrl.disable() : ctrl.enable();
    }
  }

  onValueChange(newValue: any): void {
    this.value.set(newValue);
    this.onChangeFn(newValue);
  }
}