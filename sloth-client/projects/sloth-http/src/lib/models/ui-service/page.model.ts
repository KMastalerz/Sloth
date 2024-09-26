
export interface WebPage {
    pageID: string;
    title: string;
    panels?: string[];
    class?: string;
    style?: string;
    webPanels?: WebPanel[];
}

export interface WebPanel {
    pageiD: string;
    panelID: string;
    panelType: string;
    panelLabel: string;
    sections?: string[];
    class?: string;
    style?: string;
    webSections?: WebSection[];
}

export interface WebSection {
    pageiD: string;
    panelID: string;
    sectionID: string;
    sectionType: string;
    sectionLabel: string;
    controls?: string[];
    class?: string;
    style?: string;
    webControls?: WebControl[];
}

export interface WebControl {
    pageiD: string;
    panelID: string;
    sectionID: string;
    controlID: string;
    controlType: string;
    controlLabel?: string | null;
    controlPlaceholder?: string | null;
    controlTooltip?: string | null;
    route?: string | null;
    routePageID?: string | null;
    action?: string | null;
    metaData?: string | null;
    class?: string;
    style?: string;
    isHidden: boolean;
    isReadOnly: boolean;
    isDisabled: boolean;
}

