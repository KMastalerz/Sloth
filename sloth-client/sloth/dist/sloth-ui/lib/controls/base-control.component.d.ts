import * as i0 from "@angular/core";
export declare class BaseControlComponent {
    label: import("@angular/core").InputSignal<string | null>;
    tooltip: import("@angular/core").InputSignal<string | null>;
    tooltipPosition: import("@angular/core").InputSignal<"above" | "below" | "left" | "right">;
    badge: import("@angular/core").InputSignal<string | number | null>;
    hideTooltip: import("@angular/core").Signal<boolean>;
    hideBadge: import("@angular/core").Signal<boolean>;
    static ɵfac: i0.ɵɵFactoryDeclaration<BaseControlComponent, never>;
    static ɵcmp: i0.ɵɵComponentDeclaration<BaseControlComponent, "sl-base-control", never, { "label": { "alias": "label"; "required": false; "isSignal": true; }; "tooltip": { "alias": "tooltip"; "required": false; "isSignal": true; }; "tooltipPosition": { "alias": "tooltipPosition"; "required": false; "isSignal": true; }; "badge": { "alias": "badge"; "required": false; "isSignal": true; }; }, {}, never, never, true, never>;
}
