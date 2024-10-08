
export interface WebPage {
    pageID: string;
    title: string;
    panels: string;
    orientation?: string | null;
    position?: string | null;
    background?: string | null;
    style?: string | null;
    class?: string | null;
    metaData?: string;
    webPanels?: WebPanel[];
}

export interface WebPanel {
    pageID: string;
    panelID: string;
    panelType: string;
    sections?: string | null;
    controls: string;
    style?: string | null;
    class?: string | null;
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
    internalType?: string | null;
    childControls?: string | null;
    controlLabel?: string | null;
    controlPlaceholder?: string | null;
    controlTooltip?: string | null;
    route?: string | null;
    routePageID?: string | null;
    action?: string | null;
    icon?: string | null;
    metaData?: string | null;
    validation?: string | null;
    isHidden: boolean;
    isReadOnly: boolean;
    isDisabled: boolean;
}

