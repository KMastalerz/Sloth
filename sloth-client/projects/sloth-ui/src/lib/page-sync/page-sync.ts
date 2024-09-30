import { WebControl, WebPage } from "@sloth-http";
import { BehaviorSubject } from "rxjs";
import { Action } from "./action";

export class PageSync {
    pageConfig: WebPage = {} as WebPage;
    
    toParent: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);
    toChild: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);

    getControlByID(controlID: string): WebControl {
        return this.pageConfig!.webControls?.find(c => c.controlID === controlID)!;
    }

    getControlBySectionID(sectionID: string): WebControl[] {
        return this.pageConfig!.webControls?.filter(c => c.sectionID === sectionID)!;
    }
}