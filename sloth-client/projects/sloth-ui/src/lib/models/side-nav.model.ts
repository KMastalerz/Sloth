export interface SideNavModel {
    ControlID: string;
    ControlLabel?: string | null;
    ControlTooltip?: string | null;
    Route?: string | null;
    RoutePageID?: string | null;
    Action?: string | null;
    MetaData?: string | null;
    IsHidden: boolean;
    IsReadOnly: boolean;
    IsDiabled: boolean;
    Icon: string;
    CounterFunc?: string | null;
    WarningCount?: string | null;
    ErrorCount?: string | null;
    Group: string;
}
