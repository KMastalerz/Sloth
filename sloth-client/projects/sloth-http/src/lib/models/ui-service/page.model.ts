
export interface WebPage {
    pageID: string;
    label: string;
    panels: string;
    background?: string | null;
    class?: string | null;
    hasRouter: boolean;
    layout?: string | null;
    metadata?: string;
    webPanels?: WebPanel[];
}

export interface WebPanel {
    pageID: string;
    panelID: string;
    panelType: string;
    sections: string;
    class?: string | null;
    label?: string;
    metadata?: string;
    webSections?: WebSection[];
}

export interface WebSection {
    pageID: string;
    panelID: string;
    sectionID: string;
    controls: string;
    position?: string | null;
    label?: string;
    metadata?: string;
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
    metadata?: string | null;
    validation?: string | null;
    isHidden: boolean;
    isReadOnly: boolean;
    isDisabled: boolean;
}

