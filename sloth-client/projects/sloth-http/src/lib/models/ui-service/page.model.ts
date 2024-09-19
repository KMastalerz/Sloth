import { WebControl } from "./control.model";

export interface WebPage {
    PageID: string;
    Title: string;
    WebControls?: WebControl[];
}