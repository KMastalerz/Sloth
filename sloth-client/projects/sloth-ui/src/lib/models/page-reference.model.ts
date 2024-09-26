export interface PageRef {
    pageID: string;
    page: any;
    panels: PanelRef[];
}

export interface PanelRef {
    panelID: string;
    panel: any;
    sections: SectionRef[];
}

export interface SectionRef {
    sectionID: string;
    section: any;
    controls: ControlRef[];
}

export interface ControlRef {
    controlID: string;
    control: any;
}