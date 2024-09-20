export interface SideNavMetadata {
    icon: string;
    counterFunc?: string | null;
    warningCount?: string | null;
    errorCount?: string | null;
    group: string;
}

export interface ToggleIconMetadata {
    onTrue: string;
    onFalse: string;
    size: string;
}
