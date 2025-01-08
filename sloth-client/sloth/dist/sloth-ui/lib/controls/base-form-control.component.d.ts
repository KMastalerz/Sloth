import { BaseControlComponent } from './base-control.component';
import { ControlValueAccessor, FormControl } from '@angular/forms';
import * as i0 from "@angular/core";
export declare class BaseFormControlComponent extends BaseControlComponent implements ControlValueAccessor {
    value: import("@angular/core").ModelSignal<string | number | boolean | Date | null | undefined>;
    name: import("@angular/core").InputSignal<string>;
    placeholder: import("@angular/core").InputSignal<string>;
    formControl: FormControl | undefined;
    writeValue(obj: any): void;
    registerOnChange(fn: any): void;
    registerOnTouched(fn: any): void;
    setDisabledState?(isDisabled: boolean): void;
    static ɵfac: i0.ɵɵFactoryDeclaration<BaseFormControlComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<BaseFormControlComponent, "sl-base-form-control", never, { "value": { "alias": "value"; "required": false; "isSignal": true; }; "name": { "alias": "name"; "required": false; "isSignal": true; }; "placeholder": { "alias": "placeholder"; "required": false; "isSignal": true; }; }, { "value": "valueChange"; }, never, never, true, never>;
}
