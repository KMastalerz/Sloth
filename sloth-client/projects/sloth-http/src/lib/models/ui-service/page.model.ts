
export interface WebPage {
    pageID: string;
    title: string;
    panels: string;
    metaData?: string;
    webPanels?: WebPanel[];
}

export interface WebPanel {
    pageID: string;
    panelID: string;
    panelType: string;
    sections: string;
    title?: string;
    metaData?: string;
    webSections?: WebSection[];
}

export interface WebSection {
    pageID: string;
    panelID: string;
    sectionID: string;
    controls: string;
    title?: string;
    metaData?: string;
    webControls?: WebControl[];
}

export interface WebControl {
    pageID: string;
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
    validation?: string | null;
    isHidden: boolean;
    isReadOnly: boolean;
    isDisabled: boolean;
}

