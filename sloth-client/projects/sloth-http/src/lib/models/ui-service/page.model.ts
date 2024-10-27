
export interface WebPage {
    pageID: string;
    label: string;
    panels: string;
    orientation?: string | null;
    position?: string | null;
    background?: string | null;
    class?: string | null;
    hasRouter: boolean;
    metaData?: string;
    webPanels?: WebPanel[];
}

export interface WebPanel {
    pageID: string;
    panelID: string;
    panelType: string;
    sections?: string | null;
    controls: string;
    class?: string | null;
    label?: string;
    metaData?: string;
    webControls?: WebControl[];
    webSections?: WebSection[];
}

export interface WebSection {
    pageID: string;
    panelID: string;
    sectionID: string;
    controls: string;
    label?: string;
    metaData?: string;
    webControls?: WebControl[];
}


export interface WebControl {
    pageID: string;
    panelID: string;
    sectionID: string;
    controlID: string;
    controlType: string;
    innerType?: string | null;
    style?: string | null;
    size?: string | null;
    controls?: string | null;
    label?: string | null;
    placeholder?: string | null;
    tooltip?: string | null;
    tooltipPosition?: 'above' | 'below' | 'left' | 'right' | null;
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

