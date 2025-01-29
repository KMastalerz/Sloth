import { inject, Injectable } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormGroupService {
  private readonly formBuilder = inject(FormBuilder);

  /**
   * Creates a form structure (FormGroup, FormArray, or FormControl)
   * from a plain JavaScript object (or array, or primitive).
   * It recurses over nested objects and arrays (unless skipped).
   * 
   * @param value The data object to convert into a Form structure.
   * @param skipProps An array of property names to skip deeper nesting.
   *                  If a property's name is in skipProps, it becomes a simple FormControl
   *                  holding the entire (sub)object or array (no deeper recursion).
   */
  createFormFromObject(value: any, skipProps: string[] = []): AbstractControl {
    if (Array.isArray(value)) {
      return this.createFormArray(value, skipProps);
    } else if (this.isObject(value)) {
      return this.createFormGroup(value, skipProps);
    } else {
      // Primitive => FormControl
      return new FormControl(value);
    }
  }

  // ---------------------------------------------------------
  // Internal helpers
  // ---------------------------------------------------------

  /**
   * Decide what control to create for a single property value.
   * - If 'propertyName' is in skipProps, we do NOT go deeper (just a FormControl).
   * - If it's an array, we create a FormArray (unless skipped).
   * - If it's a nested object, we create a FormGroup (unless skipped).
   * - Otherwise, a simple FormControl.
   */
  private createFormControl(
    value: any, 
    propertyName: string, 
    skipProps: string[]
  ): AbstractControl {
    if (skipProps.includes(propertyName)) {
      // If the user wants to skip deeper structure, 
      // return a FormControl holding the entire object/array
      return new FormControl(value);
    }

    // If it's an array, build a FormArray
    if (Array.isArray(value)) {
      return this.createFormArray(value, skipProps);
    }
    // If it's an object, build a FormGroup
    if (this.isObject(value)) {
      return this.createFormGroup(value, skipProps);
    }
    // Otherwise, just a FormControl
    return new FormControl(value);
  }

  private createFormArray(
    arr: any[],
    skipProps: string[]
  ): FormArray {
    const controls = arr.map((item, index) => {
      // We use `index` as the "property name" if you wanted to skip, 
      // but usually skipping by index is less common.
      return this.createFormControl(item, index.toString(), skipProps);
    });

    return this.formBuilder.array(controls);
  }

  private createFormGroup(
    obj: Record<string, any>, 
    skipProps: string[]
  ): FormGroup {
    const group: { [key: string]: AbstractControl } = {};

    for (const key of Object.keys(obj)) {
      group[key] = this.createFormControl(obj[key], key, skipProps);
    }

    return this.formBuilder.group(group);
  }

  /**
   * Check if a value is a non-null object (and not an array).
   */
  private isObject(value: any): boolean {
    return value !== null && typeof value === 'object' && !Array.isArray(value);
  }
}
