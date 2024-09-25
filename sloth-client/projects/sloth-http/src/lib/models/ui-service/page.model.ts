
export interface WebPage {
    pageID: string;
    title: string;
    panels?: string[];
    webPanels?: WebPanel[];
}

export interface WebPanel {
    panelID: string;
    panelType: string;
    panelLabel: string;
    sections?: string[];
    webSections?: WebSection[];
}

export interface WebSection {
    sectionID: string;
    sectionType: string;
    sectionLabel: string;
    controls?: string[];
    webControls?: WebControl[];
}

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

