import { WebControl, WebPage } from "@sloth-http";

export class PageSync {
    pageConfig: WebPage = {} as WebPage;
    controlsConfig: WebControl[] = [];
    pageRef: any;

    getControlByID(controlID: string): WebControl {
        return this.controlsConfig.find(c => c.controlID === controlID)!;
    }

    getControlBySectionID(sectionID: string): WebControl[] {
        return this.controlsConfig.filter(c => c.sectionID === sectionID)!;
    }
}