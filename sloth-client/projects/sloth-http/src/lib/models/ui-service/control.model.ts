export interface WebControl {
    ControlID: string;
    ControlType: string;
    ControlLabel?: string | null;
    ControlPlaceholder?: string | null;
    ControlTooltip?: string | null;
    Route?: string | null;
    RoutePageID?: string | null;
    Action?: string | null;
    MetaData?: string | null;
    IsHidden: boolean;
    IsReadOnly: boolean;
    IsDiabled: boolean;
}