import { BaseFormControlComponent } from '../../base-form-control.component';
import { ToggleListItem } from '../../../models/toggle-list-item.model';
import * as i0 from "@angular/core";
export declare class ToggleListComponent extends BaseFormControlComponent {
    items: import("@angular/core").InputSignal<ToggleListItem[]>;
    static ɵfac: i0.ɵɵFactoryDeclaration<ToggleListComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<ToggleListComponent, "sl-toggle-list", never, { "items": { "alias": "items"; "required": false; "isSignal": true; }; }, {}, never, never, true, never>;
}
