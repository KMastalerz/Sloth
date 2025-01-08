import { AfterViewInit, ElementRef } from '@angular/core';
import { BaseControlComponent } from '../../base-control.component';
import * as i0 from "@angular/core";
export declare class ToggleComponent extends BaseControlComponent implements AfterViewInit {
    themeToggle: import("@angular/core").Signal<ElementRef<any> | undefined>;
    checked: import("@angular/core").ModelSignal<boolean>;
    change: import("@angular/core").OutputEmitterRef<boolean>;
    theme: import("@angular/core").InputSignal<"theme-toggle" | null>;
    trueIcon: import("@angular/core").InputSignal<string | null>;
    falseIcon: import("@angular/core").InputSignal<string | null>;
    ngAfterViewInit(): void;
    onToggle(): void;
    static ɵfac: i0.ɵɵFactoryDeclaration<ToggleComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<ToggleComponent, "sl-toggle", never, { "checked": { "alias": "checked"; "required": false; "isSignal": true; }; "theme": { "alias": "theme"; "required": false; "isSignal": true; }; "trueIcon": { "alias": "trueIcon"; "required": false; "isSignal": true; }; "falseIcon": { "alias": "falseIcon"; "required": false; "isSignal": true; }; }, { "checked": "checkedChange"; "change": "change"; }, never, never, true, never>;
}
