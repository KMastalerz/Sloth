import * as i0 from '@angular/core';
import { input, computed, Component, output, model, viewChild, ElementRef } from '@angular/core';
import * as i1$1 from '@angular/material/button';
import { MatButtonModule } from '@angular/material/button';
import * as i2 from '@angular/material/badge';
import { MatBadgeModule } from '@angular/material/badge';
import * as i1 from '@angular/material/tooltip';
import { MatTooltipModule, MatTooltip } from '@angular/material/tooltip';
import * as i3 from '@angular/material/form-field';
import { MatFormFieldModule } from '@angular/material/form-field';
import * as i1$2 from '@angular/forms';
import { FormsModule } from '@angular/forms';
import * as i2$1 from '@angular/material/input';
import { MatInputModule } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import * as i2$2 from '@angular/material/select';
import { MatSelectModule } from '@angular/material/select';
import * as i3$1 from '@angular/material/core';
import { MatSlideToggle, MatSlideToggleModule } from '@angular/material/slide-toggle';
import * as i1$3 from '@angular/material/button-toggle';
import { MatButtonToggleModule } from '@angular/material/button-toggle';

class BaseControlComponent {
    label = input(null);
    tooltip = input(null);
    tooltipPosition = input('below');
    badge = input(null);
    hideTooltip = computed(() => !this.tooltip());
    hideBadge = computed(() => !this.badge());
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseControlComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.1.0", version: "19.0.5", type: BaseControlComponent, isStandalone: true, selector: "sl-base-control", inputs: { label: { classPropertyName: "label", publicName: "label", isSignal: true, isRequired: false, transformFunction: null }, tooltip: { classPropertyName: "tooltip", publicName: "tooltip", isSignal: true, isRequired: false, transformFunction: null }, tooltipPosition: { classPropertyName: "tooltipPosition", publicName: "tooltipPosition", isSignal: true, isRequired: false, transformFunction: null }, badge: { classPropertyName: "badge", publicName: "badge", isSignal: true, isRequired: false, transformFunction: null } }, ngImport: i0, template: '', isInline: true });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseControlComponent, decorators: [{
            type: Component,
            args: [{
                    selector: 'sl-base-control',
                    imports: [],
                    template: ''
                }]
        }] });

class ControlComponent extends BaseControlComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ControlComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: ControlComponent, isStandalone: true, selector: "sl-control", usesInheritance: true, ngImport: i0, template: "<div [matTooltipDisabled]=\"hideTooltip()\"\r\n     [matTooltip]=\"tooltip()\"\r\n     [matTooltipPosition]=\"tooltipPosition()\"   \r\n     [matBadge]=\"badge()\" \r\n     [matBadgeHidden]=\"hideBadge()\">\r\n    <ng-content/>\r\n</div>", styles: [""], dependencies: [{ kind: "ngmodule", type: MatTooltipModule }, { kind: "directive", type: i1.MatTooltip, selector: "[matTooltip]", inputs: ["matTooltipPosition", "matTooltipPositionAtOrigin", "matTooltipDisabled", "matTooltipShowDelay", "matTooltipHideDelay", "matTooltipTouchGestures", "matTooltip", "matTooltipClass"], exportAs: ["matTooltip"] }, { kind: "ngmodule", type: MatBadgeModule }, { kind: "directive", type: i2.MatBadge, selector: "[matBadge]", inputs: ["matBadgeColor", "matBadgeOverlap", "matBadgeDisabled", "matBadgePosition", "matBadge", "matBadgeDescription", "matBadgeSize", "matBadgeHidden"] }, { kind: "ngmodule", type: MatFormFieldModule }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ControlComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-control', imports: [MatTooltipModule, MatTooltip, MatBadgeModule, MatFormFieldModule], template: "<div [matTooltipDisabled]=\"hideTooltip()\"\r\n     [matTooltip]=\"tooltip()\"\r\n     [matTooltipPosition]=\"tooltipPosition()\"   \r\n     [matBadge]=\"badge()\" \r\n     [matBadgeHidden]=\"hideBadge()\">\r\n    <ng-content/>\r\n</div>" }]
        }] });

class BaseButtonComponent extends BaseControlComponent {
    onClick = output();
    onClickEmit() {
        this.onClick.emit();
    }
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseButtonComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: BaseButtonComponent, isStandalone: true, selector: "sl-base-button", outputs: { onClick: "onClick" }, usesInheritance: true, ngImport: i0, template: '', isInline: true });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseButtonComponent, decorators: [{
            type: Component,
            args: [{
                    selector: 'sl-base-button',
                    imports: [],
                    template: '',
                }]
        }] });

class FlatButtonComponent extends BaseButtonComponent {
    isHeader = input(false);
    type = input(null);
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: FlatButtonComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.1.0", version: "19.0.5", type: FlatButtonComponent, isStandalone: true, selector: "sl-flat-button", inputs: { isHeader: { classPropertyName: "isHeader", publicName: "isHeader", isSignal: true, isRequired: false, transformFunction: null }, type: { classPropertyName: "type", publicName: "type", isSignal: true, isRequired: false, transformFunction: null } }, usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <button mat-flat-button \r\n            (click)=\"onClickEmit()\"\r\n            [class]=\"\r\n            {\r\n                'header-button': isHeader(),\r\n                'add-button': type() === 'add',\r\n                'delete-button': type() === 'delete',\r\n            }\"> \r\n        {{ label() }}\r\n    </button>\r\n</sl-control>\r\n", styles: [".add-button,.delete-button{background-color:#167533!important;color:#fff!important}.delete-button{background-color:#851d22!important}.header-button{padding:.02rem .4rem;border-radius:.4rem;font-weight:500;font-size:small;height:auto}\n"], dependencies: [{ kind: "ngmodule", type: MatButtonModule }, { kind: "component", type: i1$1.MatButton, selector: "    button[mat-button], button[mat-raised-button], button[mat-flat-button],    button[mat-stroked-button]  ", exportAs: ["matButton"] }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: FlatButtonComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-flat-button', imports: [MatButtonModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <button mat-flat-button \r\n            (click)=\"onClickEmit()\"\r\n            [class]=\"\r\n            {\r\n                'header-button': isHeader(),\r\n                'add-button': type() === 'add',\r\n                'delete-button': type() === 'delete',\r\n            }\"> \r\n        {{ label() }}\r\n    </button>\r\n</sl-control>\r\n", styles: [".add-button,.delete-button{background-color:#167533!important;color:#fff!important}.delete-button{background-color:#851d22!important}.header-button{padding:.02rem .4rem;border-radius:.4rem;font-weight:500;font-size:small;height:auto}\n"] }]
        }] });

class FlatIconButtonComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: FlatIconButtonComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: FlatIconButtonComponent, isStandalone: true, selector: "sl-flat-icon-button", ngImport: i0, template: "<p>flat-icon-button works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: FlatIconButtonComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-flat-icon-button', imports: [], template: "<p>flat-icon-button works!</p>\r\n" }]
        }] });

class IconButtonComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: IconButtonComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: IconButtonComponent, isStandalone: true, selector: "sl-icon-button", ngImport: i0, template: "<p>icon-button works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: IconButtonComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-icon-button', imports: [], template: "<p>icon-button works!</p>\r\n" }]
        }] });

class DatePickerComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: DatePickerComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: DatePickerComponent, isStandalone: true, selector: "sl-date-picker", ngImport: i0, template: "<p>date-picker works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: DatePickerComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-date-picker', imports: [], template: "<p>date-picker works!</p>\r\n" }]
        }] });

class DateTimePickerComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: DateTimePickerComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: DateTimePickerComponent, isStandalone: true, selector: "sl-date-time-picker", ngImport: i0, template: "<p>date-time-picker works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: DateTimePickerComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-date-time-picker', imports: [], template: "<p>date-time-picker works!</p>\r\n" }]
        }] });

class EmailInputComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: EmailInputComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: EmailInputComponent, isStandalone: true, selector: "sl-email-input", ngImport: i0, template: "<p>email-input works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: EmailInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-email-input', imports: [], template: "<p>email-input works!</p>\r\n" }]
        }] });

class BaseFormControlComponent extends BaseControlComponent {
    value = model();
    name = input('');
    placeholder = input('');
    formControl;
    writeValue(obj) {
        this.formControl?.setValue(obj); // Make sure this is setting a valid primitive
    }
    registerOnChange(fn) {
        this.formControl?.valueChanges.subscribe(fn);
    }
    registerOnTouched(fn) { }
    setDisabledState(isDisabled) { }
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseFormControlComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.1.0", version: "19.0.5", type: BaseFormControlComponent, isStandalone: true, selector: "sl-base-form-control", inputs: { value: { classPropertyName: "value", publicName: "value", isSignal: true, isRequired: false, transformFunction: null }, name: { classPropertyName: "name", publicName: "name", isSignal: true, isRequired: false, transformFunction: null }, placeholder: { classPropertyName: "placeholder", publicName: "placeholder", isSignal: true, isRequired: false, transformFunction: null } }, outputs: { value: "valueChange" }, usesInheritance: true, ngImport: i0, template: '', isInline: true });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: BaseFormControlComponent, decorators: [{
            type: Component,
            args: [{
                    selector: 'sl-base-form-control',
                    imports: [],
                    template: ''
                }]
        }] });

class MarkupInputComponent extends BaseFormControlComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: MarkupInputComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: MarkupInputComponent, isStandalone: true, selector: "sl-markup-input", usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <textarea matInput [(ngModel)]=\"value\" [name]=\"name()!\" [placeholder]=\"placeholder()\">\r\n        </textarea>\r\n    </mat-form-field>\r\n</sl-control>\r\n\r\n", styles: ["mat-form-field{width:100%}\n"], dependencies: [{ kind: "ngmodule", type: FormsModule }, { kind: "directive", type: i1$2.DefaultValueAccessor, selector: "input:not([type=checkbox])[formControlName],textarea[formControlName],input:not([type=checkbox])[formControl],textarea[formControl],input:not([type=checkbox])[ngModel],textarea[ngModel],[ngDefaultControl]" }, { kind: "directive", type: i1$2.NgControlStatus, selector: "[formControlName],[ngModel],[formControl]" }, { kind: "directive", type: i1$2.NgModel, selector: "[ngModel]:not([formControlName]):not([formControl])", inputs: ["name", "disabled", "ngModel", "ngModelOptions"], outputs: ["ngModelChange"], exportAs: ["ngModel"] }, { kind: "ngmodule", type: MatInputModule }, { kind: "directive", type: i2$1.MatInput, selector: "input[matInput], textarea[matInput], select[matNativeControl],      input[matNativeControl], textarea[matNativeControl]", inputs: ["disabled", "id", "placeholder", "name", "required", "type", "errorStateMatcher", "aria-describedby", "value", "readonly", "disabledInteractive"], exportAs: ["matInput"] }, { kind: "component", type: i3.MatFormField, selector: "mat-form-field", inputs: ["hideRequiredMarker", "color", "floatLabel", "appearance", "subscriptSizing", "hintLabel"], exportAs: ["matFormField"] }, { kind: "directive", type: i3.MatLabel, selector: "mat-label" }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: MarkupInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-markup-input', imports: [FormsModule, MatInputModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <textarea matInput [(ngModel)]=\"value\" [name]=\"name()!\" [placeholder]=\"placeholder()\">\r\n        </textarea>\r\n    </mat-form-field>\r\n</sl-control>\r\n\r\n", styles: ["mat-form-field{width:100%}\n"] }]
        }] });

class NumberInputComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: NumberInputComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: NumberInputComponent, isStandalone: true, selector: "sl-number-input", ngImport: i0, template: "<p>number-input works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: NumberInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-number-input', imports: [], template: "<p>number-input works!</p>\r\n" }]
        }] });

class PasswordInputComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: PasswordInputComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: PasswordInputComponent, isStandalone: true, selector: "sl-password-input", ngImport: i0, template: "<p>password-input works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: PasswordInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-password-input', imports: [], template: "<p>password-input works!</p>\r\n" }]
        }] });

class PhoneInputComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: PhoneInputComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: PhoneInputComponent, isStandalone: true, selector: "sl-phone-input", ngImport: i0, template: "<p>phone-input works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: PhoneInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-phone-input', imports: [], template: "<p>phone-input works!</p>\r\n" }]
        }] });

class TextInputComponent extends BaseFormControlComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TextInputComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: TextInputComponent, isStandalone: true, selector: "sl-text-input", usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <input matInput [(ngModel)]=\"value\" [name]=\"name()!\" [placeholder]=\"placeholder()\"/>\r\n    </mat-form-field>\r\n</sl-control>\r\n\r\n", styles: ["mat-form-field{width:100%}\n"], dependencies: [{ kind: "ngmodule", type: FormsModule }, { kind: "directive", type: i1$2.DefaultValueAccessor, selector: "input:not([type=checkbox])[formControlName],textarea[formControlName],input:not([type=checkbox])[formControl],textarea[formControl],input:not([type=checkbox])[ngModel],textarea[ngModel],[ngDefaultControl]" }, { kind: "directive", type: i1$2.NgControlStatus, selector: "[formControlName],[ngModel],[formControl]" }, { kind: "directive", type: i1$2.NgModel, selector: "[ngModel]:not([formControlName]):not([formControl])", inputs: ["name", "disabled", "ngModel", "ngModelOptions"], outputs: ["ngModelChange"], exportAs: ["ngModel"] }, { kind: "ngmodule", type: MatInputModule }, { kind: "directive", type: i2$1.MatInput, selector: "input[matInput], textarea[matInput], select[matNativeControl],      input[matNativeControl], textarea[matNativeControl]", inputs: ["disabled", "id", "placeholder", "name", "required", "type", "errorStateMatcher", "aria-describedby", "value", "readonly", "disabledInteractive"], exportAs: ["matInput"] }, { kind: "component", type: i3.MatFormField, selector: "mat-form-field", inputs: ["hideRequiredMarker", "color", "floatLabel", "appearance", "subscriptSizing", "hintLabel"], exportAs: ["matFormField"] }, { kind: "directive", type: i3.MatLabel, selector: "mat-label" }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TextInputComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-text-input', imports: [FormsModule, MatInputModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <input matInput [(ngModel)]=\"value\" [name]=\"name()!\" [placeholder]=\"placeholder()\"/>\r\n    </mat-form-field>\r\n</sl-control>\r\n\r\n", styles: ["mat-form-field{width:100%}\n"] }]
        }] });

class TimePickerComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TimePickerComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: TimePickerComponent, isStandalone: true, selector: "sl-time-picker", ngImport: i0, template: "<p>time-picker works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TimePickerComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-time-picker', imports: [], template: "<p>time-picker works!</p>\r\n" }]
        }] });

class ButtonLinkComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ButtonLinkComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: ButtonLinkComponent, isStandalone: true, selector: "sl-button-link", ngImport: i0, template: "<p>button-link works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ButtonLinkComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-button-link', imports: [], template: "<p>button-link works!</p>\r\n" }]
        }] });

class HeaderLinkComponent extends BaseControlComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: HeaderLinkComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: HeaderLinkComponent, isStandalone: true, selector: "sl-header-link", usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <a mat-button>\r\n    @if(label()) {\r\n        {{ label() }}\r\n    }\r\n    </a>\r\n</sl-control>\r\n", styles: ["a{padding:.1rem .6rem!important;border-radius:.4rem!important;padding:.1rem .6rem;height:auto;font-weight:500;font-size:small}a:hover{padding:.1rem .6rem!important;border-radius:.4rem!important}\n"], dependencies: [{ kind: "ngmodule", type: MatButtonModule }, { kind: "component", type: i1$1.MatAnchor, selector: "a[mat-button], a[mat-raised-button], a[mat-flat-button], a[mat-stroked-button]", exportAs: ["matButton", "matAnchor"] }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: HeaderLinkComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-header-link', imports: [MatButtonModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <a mat-button>\r\n    @if(label()) {\r\n        {{ label() }}\r\n    }\r\n    </a>\r\n</sl-control>\r\n", styles: ["a{padding:.1rem .6rem!important;border-radius:.4rem!important;padding:.1rem .6rem;height:auto;font-weight:500;font-size:small}a:hover{padding:.1rem .6rem!important;border-radius:.4rem!important}\n"] }]
        }] });

class NavigationLinkComponent extends BaseControlComponent {
    icon = input.required();
    value = input(undefined);
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: NavigationLinkComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: NavigationLinkComponent, isStandalone: true, selector: "sl-navigation-link", inputs: { icon: { classPropertyName: "icon", publicName: "icon", isSignal: true, isRequired: true, transformFunction: null }, value: { classPropertyName: "value", publicName: "value", isSignal: true, isRequired: false, transformFunction: null } }, usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <a mat-button>\r\n        <mat-icon>{{icon()}}</mat-icon>\r\n        @if(label()) {\r\n            {{label()}}\r\n        }\r\n        @if(value()) {\r\n            <span class=\"tag\">{{ value() }}</span>\r\n        }\r\n    </a>\r\n</sl-control>", styles: ["a{align-items:center;justify-content:flex-start;color:var(--mat-sys-on-surface-variant)!important}.tag{background-color:var(--mat-sys-error);padding:.2rem .4rem;border-radius:.4rem;font-size:.8rem;margin-left:.8rem;font-weight:700;color:var(--mat-sys-surface)}\n"], dependencies: [{ kind: "component", type: MatIcon, selector: "mat-icon", inputs: ["color", "inline", "svgIcon", "fontSet", "fontIcon"], exportAs: ["matIcon"] }, { kind: "ngmodule", type: MatButtonModule }, { kind: "component", type: i1$1.MatAnchor, selector: "a[mat-button], a[mat-raised-button], a[mat-flat-button], a[mat-stroked-button]", exportAs: ["matButton", "matAnchor"] }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: NavigationLinkComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-navigation-link', imports: [MatIcon, MatButtonModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <a mat-button>\r\n        <mat-icon>{{icon()}}</mat-icon>\r\n        @if(label()) {\r\n            {{label()}}\r\n        }\r\n        @if(value()) {\r\n            <span class=\"tag\">{{ value() }}</span>\r\n        }\r\n    </a>\r\n</sl-control>", styles: ["a{align-items:center;justify-content:flex-start;color:var(--mat-sys-on-surface-variant)!important}.tag{background-color:var(--mat-sys-error);padding:.2rem .4rem;border-radius:.4rem;font-size:.8rem;margin-left:.8rem;font-weight:700;color:var(--mat-sys-surface)}\n"] }]
        }] });

class RegularLinkComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: RegularLinkComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: RegularLinkComponent, isStandalone: true, selector: "sl-regular-link", ngImport: i0, template: "<p>regular-link works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: RegularLinkComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-regular-link', imports: [], template: "<p>regular-link works!</p>\r\n" }]
        }] });

class CheckboxComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: CheckboxComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: CheckboxComponent, isStandalone: true, selector: "sl-checkbox", ngImport: i0, template: "<p>checkbox works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: CheckboxComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-checkbox', imports: [], template: "<p>checkbox works!</p>\r\n" }]
        }] });

class ChipComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ChipComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: ChipComponent, isStandalone: true, selector: "sl-chip", ngImport: i0, template: "<p>chip works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ChipComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-chip', imports: [], template: "<p>chip works!</p>\r\n" }]
        }] });

class ListSelectComponent extends BaseFormControlComponent {
    multiple = input(false);
    items = input([]);
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ListSelectComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: ListSelectComponent, isStandalone: true, selector: "sl-list-select", inputs: { multiple: { classPropertyName: "multiple", publicName: "multiple", isSignal: true, isRequired: false, transformFunction: null }, items: { classPropertyName: "items", publicName: "items", isSignal: true, isRequired: false, transformFunction: null } }, usesInheritance: true, ngImport: i0, template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <mat-select [multiple]=\"multiple()\" [(ngModel)]=\"value\" [name]=\"name()\">\r\n            @for (item of items(); track item) {\r\n                <mat-option [value]=\"item.value\">{{item.display ?? item.value}}</mat-option>\r\n            }\r\n        </mat-select>\r\n    </mat-form-field>\r\n</sl-control>\r\n", styles: ["mat-form-field{width:100%}\n"], dependencies: [{ kind: "ngmodule", type: MatFormFieldModule }, { kind: "component", type: i3.MatFormField, selector: "mat-form-field", inputs: ["hideRequiredMarker", "color", "floatLabel", "appearance", "subscriptSizing", "hintLabel"], exportAs: ["matFormField"] }, { kind: "directive", type: i3.MatLabel, selector: "mat-label" }, { kind: "ngmodule", type: MatSelectModule }, { kind: "component", type: i2$2.MatSelect, selector: "mat-select", inputs: ["aria-describedby", "panelClass", "disabled", "disableRipple", "tabIndex", "hideSingleSelectionIndicator", "placeholder", "required", "multiple", "disableOptionCentering", "compareWith", "value", "aria-label", "aria-labelledby", "errorStateMatcher", "typeaheadDebounceInterval", "sortComparator", "id", "panelWidth", "canSelectNullableOptions"], outputs: ["openedChange", "opened", "closed", "selectionChange", "valueChange"], exportAs: ["matSelect"] }, { kind: "component", type: i3$1.MatOption, selector: "mat-option", inputs: ["value", "id", "disabled"], outputs: ["onSelectionChange"], exportAs: ["matOption"] }, { kind: "ngmodule", type: FormsModule }, { kind: "directive", type: i1$2.NgControlStatus, selector: "[formControlName],[ngModel],[formControl]" }, { kind: "directive", type: i1$2.NgModel, selector: "[ngModel]:not([formControlName]):not([formControl])", inputs: ["name", "disabled", "ngModel", "ngModelOptions"], outputs: ["ngModelChange"], exportAs: ["ngModel"] }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ListSelectComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-list-select', imports: [MatFormFieldModule, MatSelectModule, FormsModule, ControlComponent], template: "<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-form-field appearance=\"outline\">\r\n        @if( label() ) {\r\n            <mat-label>{{ label() }}</mat-label>\r\n        }\r\n        <mat-select [multiple]=\"multiple()\" [(ngModel)]=\"value\" [name]=\"name()\">\r\n            @for (item of items(); track item) {\r\n                <mat-option [value]=\"item.value\">{{item.display ?? item.value}}</mat-option>\r\n            }\r\n        </mat-select>\r\n    </mat-form-field>\r\n</sl-control>\r\n", styles: ["mat-form-field{width:100%}\n"] }]
        }] });

class RadioSelectComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: RadioSelectComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: RadioSelectComponent, isStandalone: true, selector: "sl-radio-select", ngImport: i0, template: "<p>radio-select works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: RadioSelectComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-radio-select', imports: [], template: "<p>radio-select works!</p>\r\n" }]
        }] });

class TagComponent {
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TagComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "14.0.0", version: "19.0.5", type: TagComponent, isStandalone: true, selector: "sl-tag", ngImport: i0, template: "<p>tag works!</p>\r\n", styles: [""] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: TagComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-tag', imports: [], template: "<p>tag works!</p>\r\n" }]
        }] });

class ToggleComponent extends BaseControlComponent {
    themeToggle = viewChild(MatSlideToggle, { read: ElementRef });
    checked = model(false);
    change = output();
    theme = input(null);
    trueIcon = input(null);
    falseIcon = input(null);
    ngAfterViewInit() {
        if (this.themeToggle()) {
            if (this.trueIcon())
                this.themeToggle().nativeElement.querySelector('.mdc-switch__icon--on').firstChild.setAttribute('d', this.trueIcon());
            if (this.falseIcon())
                this.themeToggle().nativeElement.querySelector('.mdc-switch__icon--off').firstChild.setAttribute('d', this.falseIcon());
        }
    }
    onToggle() {
        this.checked.set(!this.checked());
        this.change.emit(this.checked());
    }
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ToggleComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.2.0", version: "19.0.5", type: ToggleComponent, isStandalone: true, selector: "sl-toggle", inputs: { checked: { classPropertyName: "checked", publicName: "checked", isSignal: true, isRequired: false, transformFunction: null }, theme: { classPropertyName: "theme", publicName: "theme", isSignal: true, isRequired: false, transformFunction: null }, trueIcon: { classPropertyName: "trueIcon", publicName: "trueIcon", isSignal: true, isRequired: false, transformFunction: null }, falseIcon: { classPropertyName: "falseIcon", publicName: "falseIcon", isSignal: true, isRequired: false, transformFunction: null } }, outputs: { checked: "checkedChange", change: "change" }, viewQueries: [{ propertyName: "themeToggle", first: true, predicate: MatSlideToggle, descendants: true, read: ElementRef, isSignal: true }], usesInheritance: true, ngImport: i0, template: "\r\n<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-slide-toggle [checked]=\"checked()\" \r\n                      (change)=\"onToggle()\"\r\n                      [class]=\"{\r\n                        'theme-toggle': theme() === 'theme-toggle',\r\n                      }\"/>\r\n</sl-control>", styles: [".theme-toggle.mat-mdc-slide-toggle{--mdc-switch-selected-focus-state-layer-color: #D4AC0D;--mdc-switch-selected-handle-color: #DDBD3D;--mdc-switch-selected-hover-state-layer-color: #D4AC0D;--mdc-switch-selected-pressed-state-layer-color: #D4AC0D;--mdc-switch-selected-focus-handle-color: #D4AC0D;--mdc-switch-selected-hover-handle-color: #D4AC0D;--mdc-switch-selected-pressed-handle-color: #D4AC0D;--mdc-switch-selected-focus-track-color: #A4DDEF;--mdc-switch-selected-hover-track-color: #A4DDEF;--mdc-switch-selected-pressed-track-color: #A4DDEF;--mdc-switch-selected-track-color: #A4DDEF}\n"], dependencies: [{ kind: "component", type: MatSlideToggle, selector: "mat-slide-toggle", inputs: ["name", "id", "labelPosition", "aria-label", "aria-labelledby", "aria-describedby", "required", "color", "disabled", "disableRipple", "tabIndex", "checked", "hideIcon", "disabledInteractive"], outputs: ["change", "toggleChange"], exportAs: ["matSlideToggle"] }, { kind: "ngmodule", type: MatSlideToggleModule }, { kind: "component", type: ControlComponent, selector: "sl-control" }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ToggleComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-toggle', imports: [MatSlideToggle, MatSlideToggleModule, ControlComponent], template: "\r\n<sl-control [tooltip]=\"tooltip()\"\r\n            [tooltipPosition]=\"tooltipPosition()\"\r\n            [badge]=\"badge()\">\r\n    <mat-slide-toggle [checked]=\"checked()\" \r\n                      (change)=\"onToggle()\"\r\n                      [class]=\"{\r\n                        'theme-toggle': theme() === 'theme-toggle',\r\n                      }\"/>\r\n</sl-control>", styles: [".theme-toggle.mat-mdc-slide-toggle{--mdc-switch-selected-focus-state-layer-color: #D4AC0D;--mdc-switch-selected-handle-color: #DDBD3D;--mdc-switch-selected-hover-state-layer-color: #D4AC0D;--mdc-switch-selected-pressed-state-layer-color: #D4AC0D;--mdc-switch-selected-focus-handle-color: #D4AC0D;--mdc-switch-selected-hover-handle-color: #D4AC0D;--mdc-switch-selected-pressed-handle-color: #D4AC0D;--mdc-switch-selected-focus-track-color: #A4DDEF;--mdc-switch-selected-hover-track-color: #A4DDEF;--mdc-switch-selected-pressed-track-color: #A4DDEF;--mdc-switch-selected-track-color: #A4DDEF}\n"] }]
        }] });

class ToggleListComponent extends BaseFormControlComponent {
    items = input([]);
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ToggleListComponent, deps: null, target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: ToggleListComponent, isStandalone: true, selector: "sl-toggle-list", inputs: { items: { classPropertyName: "items", publicName: "items", isSignal: true, isRequired: false, transformFunction: null } }, usesInheritance: true, ngImport: i0, template: "<div class=\"toggle-list-container\">\r\n    @if(label()) {\r\n        <label>{{ label() }}</label>\r\n    }\r\n    \r\n    <mat-button-toggle-group [(ngModel)]=\"value\" name=\"name()!\">\r\n        @for(item of items(); track item) {\r\n            <mat-button-toggle [value]=\"item.value\" [class]=\"item.class\">{{ item.display ?? item.value }}</mat-button-toggle>\r\n        }\r\n    </mat-button-toggle-group>\r\n</div>\r\n", styles: [".toggle-list-container{display:flex;flex-direction:column;gap:.4rem}.critical.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #fafafa;background-color:light-dark(#9D4A4E,#851d22)}.high.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: light-dark(#121212, #fafafa);background-color:light-dark(#af7f3c,#71532a)}.medium.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:#f0ad4e}.low.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:light-dark(#4DBC7B,#009f42)}.lowest.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:light-dark(#60A3FB,#388cfa)}\n"], dependencies: [{ kind: "ngmodule", type: MatButtonToggleModule }, { kind: "directive", type: i1$3.MatButtonToggleGroup, selector: "mat-button-toggle-group", inputs: ["appearance", "name", "vertical", "value", "multiple", "disabled", "disabledInteractive", "hideSingleSelectionIndicator", "hideMultipleSelectionIndicator"], outputs: ["valueChange", "change"], exportAs: ["matButtonToggleGroup"] }, { kind: "component", type: i1$3.MatButtonToggle, selector: "mat-button-toggle", inputs: ["aria-label", "aria-labelledby", "id", "name", "value", "tabIndex", "disableRipple", "appearance", "checked", "disabled", "disabledInteractive"], outputs: ["change"], exportAs: ["matButtonToggle"] }, { kind: "ngmodule", type: FormsModule }, { kind: "directive", type: i1$2.NgControlStatus, selector: "[formControlName],[ngModel],[formControl]" }, { kind: "directive", type: i1$2.NgModel, selector: "[ngModel]:not([formControlName]):not([formControl])", inputs: ["name", "disabled", "ngModel", "ngModelOptions"], outputs: ["ngModelChange"], exportAs: ["ngModel"] }] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: ToggleListComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-toggle-list', imports: [MatButtonToggleModule, FormsModule], template: "<div class=\"toggle-list-container\">\r\n    @if(label()) {\r\n        <label>{{ label() }}</label>\r\n    }\r\n    \r\n    <mat-button-toggle-group [(ngModel)]=\"value\" name=\"name()!\">\r\n        @for(item of items(); track item) {\r\n            <mat-button-toggle [value]=\"item.value\" [class]=\"item.class\">{{ item.display ?? item.value }}</mat-button-toggle>\r\n        }\r\n    </mat-button-toggle-group>\r\n</div>\r\n", styles: [".toggle-list-container{display:flex;flex-direction:column;gap:.4rem}.critical.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #fafafa;background-color:light-dark(#9D4A4E,#851d22)}.high.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: light-dark(#121212, #fafafa);background-color:light-dark(#af7f3c,#71532a)}.medium.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:#f0ad4e}.low.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:light-dark(#4DBC7B,#009f42)}.lowest.mat-button-toggle-appearance-standard.mat-button-toggle-checked{--mat-sys-on-secondary-container: #121212;background-color:light-dark(#60A3FB,#388cfa)}\n"] }]
        }] });

const moon = 'M12 15.5q1.45 0 2.475-1.025Q15.5 13.45 15.5 12q0-1.45-1.025-2.475Q13.45 8.5 12 8.5q-1.45 0-2.475 1.025Q8.5 10.55 8.5 12q0 1.45 1.025 2.475Q10.55 15.5 12 15.5Zm0 1.5q-2.075 0-3.537-1.463T7 12q0-2.075 1.463-3.537T12 7q2.075 0 3.537 1.463T17 12q0 2.075-1.463 3.537T12 17ZM1.75 12.75q-.325 0-.538-.213Q1 12.325 1 12q0-.325.212-.537Q1.425 11.25 1.75 11.25h2.5q.325 0 .537.213Q5 11.675 5 12q0 .325-.213.537-.213.213-.537.213Zm18 0q-.325 0-.538-.213Q19 12.325 19 12q0-.325.212-.537.212-.213.538-.213h2.5q.325 0 .538.213Q23 11.675 23 12q0 .325-.212.537-.212.213-.538.213ZM12 5q-.325 0-.537-.213Q11.25 4.575 11.25 4.25v-2.5q0-.325.213-.538Q11.675 1 12 1q.325 0 .537.212 .213.212 .213.538v2.5q0 .325-.213.537Q12.325 5 12 5Zm0 18q-.325 0-.537-.212-.213-.212-.213-.538v-2.5q0-.325.213-.538Q11.675 19 12 19q.325 0 .537.212 .213.212 .213.538v2.5q0 .325-.213.538Q12.325 23 12 23ZM6 7.05l-1.425-1.4q-.225-.225-.213-.537.013-.312.213-.537.225-.225.537-.225t.537.225L7.05 6q.2.225 .2.525 0 .3-.2.5-.2.225-.513.225-.312 0-.537-.2Zm12.35 12.375L16.95 18q-.2-.225-.2-.538t.225-.512q.2-.225.5-.225t.525.225l1.425 1.4q.225.225 .212.538-.012.313-.212.538-.225.225-.538.225t-.538-.225ZM16.95 7.05q-.225-.225-.225-.525 0-.3.225-.525l1.4-1.425q.225-.225.538-.213.313 .013.538 .213.225 .225.225 .537t-.225.537L18 7.05q-.2.2-.512.2-.312 0-.538-.2ZM4.575 19.425q-.225-.225-.225-.538t.225-.538L6 16.95q.225-.225.525-.225.3 0 .525.225 .225.225 .225.525 0 .3-.225.525l-1.4 1.425q-.225.225-.537.212-.312-.012-.537-.212ZM12 12Z';

const sun = 'M12 21q-3.75 0-6.375-2.625T3 12q0-3.75 2.625-6.375T12 3q.2 0 .425.013 .225.013 .575.038-.9.8-1.4 1.975-.5 1.175-.5 2.475 0 2.25 1.575 3.825Q14.25 12.9 16.5 12.9q1.3 0 2.475-.463T20.95 11.15q.025.3 .038.488Q21 11.825 21 12q0 3.75-2.625 6.375T12 21Zm0-1.5q2.725 0 4.75-1.687t2.525-3.963q-.625.275-1.337.412Q17.225 14.4 16.5 14.4q-2.875 0-4.887-2.013T9.6 7.5q0-.6.125-1.287.125-.687.45-1.562-2.45.675-4.062 2.738Q4.5 9.45 4.5 12q0 3.125 2.188 5.313T12 19.5Zm-.1-7.425Z';

class SectionComponent {
    header = input();
    static ɵfac = i0.ɵɵngDeclareFactory({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: SectionComponent, deps: [], target: i0.ɵɵFactoryTarget.Component });
    static ɵcmp = i0.ɵɵngDeclareComponent({ minVersion: "17.0.0", version: "19.0.5", type: SectionComponent, isStandalone: true, selector: "sl-section", inputs: { header: { classPropertyName: "header", publicName: "header", isSignal: true, isRequired: false, transformFunction: null } }, ngImport: i0, template: "<section>\r\n    @if(header()) {\r\n        <p>{{header()}}</p>\r\n    }\r\n    <ng-content></ng-content>\r\n</section>", styles: ["p{margin-bottom:.4rem}\n"] });
}
i0.ɵɵngDeclareClassMetadata({ minVersion: "12.0.0", version: "19.0.5", ngImport: i0, type: SectionComponent, decorators: [{
            type: Component,
            args: [{ selector: 'sl-section', imports: [], template: "<section>\r\n    @if(header()) {\r\n        <p>{{header()}}</p>\r\n    }\r\n    <ng-content></ng-content>\r\n</section>", styles: ["p{margin-bottom:.4rem}\n"] }]
        }] });

/*
 * Public API Surface of sloth-ui
 */
/* Controls */

/**
 * Generated bundle index. Do not edit.
 */

export { ButtonLinkComponent, CheckboxComponent, ChipComponent, DatePickerComponent, DateTimePickerComponent, EmailInputComponent, FlatButtonComponent, FlatIconButtonComponent, HeaderLinkComponent, IconButtonComponent, ListSelectComponent, MarkupInputComponent, NavigationLinkComponent, NumberInputComponent, PasswordInputComponent, PhoneInputComponent, RadioSelectComponent, RegularLinkComponent, SectionComponent, TagComponent, TextInputComponent, TimePickerComponent, ToggleComponent, ToggleListComponent, moon, sun };
//# sourceMappingURL=sloth-ui.mjs.map
