import { WebControl } from "@sloth-http";

export interface WebSection { 
    sectionID: string;
    webControls: WebControl[];
}