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

export interface ButtonMetadata {

}

export interface LinkMetadata {
    type: string;
    icon: string;
    counterFunc?: string;
    warningCount?: number;
    errorCount?: number;
}