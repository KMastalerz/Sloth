import { BaseButtonComponent } from '../base-button.component';
import * as i0 from "@angular/core";
export declare class FlatButtonComponent extends BaseButtonComponent {
    isHeader: import("@angular/core").InputSignal<boolean>;
    type: import("@angular/core").InputSignal<"add" | "delete" | null>;
    static ɵfac: i0.ɵɵFactoryDeclaration<FlatButtonComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<FlatButtonComponent, "sl-flat-button", never, { "isHeader": { "alias": "isHeader"; "required": false; "isSignal": true; }; "type": { "alias": "type"; "required": false; "isSignal": true; }; }, {}, never, never, true, never>;
}
