import { WebControl } from "./control.model";

export interface WebPage {
    pageID: string;
    title: string;
    webControls?: WebControl[];
}