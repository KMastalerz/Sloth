import { BaseFormControlComponent } from '../../base-form-control.component';
import { ListSelectItem } from '../../../models/list-select-item.model';
import * as i0 from "@angular/core";
export declare class ListSelectComponent extends BaseFormControlComponent {
    multiple: import("@angular/core").InputSignal<boolean>;
    items: import("@angular/core").InputSignal<ListSelectItem[]>;
    static ɵfac: i0.ɵɵFactoryDeclaration<ListSelectComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<ListSelectComponent, "sl-list-select", never, { "multiple": { "alias": "multiple"; "required": false; "isSignal": true; }; "items": { "alias": "items"; "required": false; "isSignal": true; }; }, {}, never, never, true, never>;
}
