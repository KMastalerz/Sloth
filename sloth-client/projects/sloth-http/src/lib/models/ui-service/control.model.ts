export interface WebControl {
    controlID: string;
    controlType: string;
    controlLabel?: string | null;
    controlPlaceholder?: string | null;
    controlTooltip?: string | null;
    route?: string | null;
    routePageID?: string | null;
    action?: string | null;
    metaData?: string | null;
    isHidden: boolean;
    isReadOnly: boolean;
    isDisabled: boolean;
}