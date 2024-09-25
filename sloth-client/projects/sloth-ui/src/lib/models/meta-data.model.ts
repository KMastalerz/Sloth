export interface InputMetadata {
    icon?: string;
}

export interface PasswordMetadata {
    icon?: string;
    iconShow?: string;
    iconHide?: string;
}

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