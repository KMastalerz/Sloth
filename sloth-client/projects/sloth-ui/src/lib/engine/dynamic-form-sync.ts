import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { WebControl, WebPage, WebPanel, WebSection } from "@sloth-http";
import { PageRef, PanelRef, SectionRef } from "../models/page-reference.model";

export class DynamicFormSync {
    panelMap: Map<string, WebPanel> = new Map<string, WebPanel>();
    sectionMap: Map<string, WebSection> = new Map<string, WebSection>();
    controlMap: Map<string, WebControl> = new Map<string, WebControl>();
    panelFormMap: Map<string, FormGroup | FormArray> = new Map<string, FormGroup | FormArray>();
    controlFormMap: Map<string, FormControl> = new Map<string, FormControl>();
    pageForm: FormGroup = new FormGroup({});
    pageConfig: WebPage = {} as WebPage;
    pageRef: PageRef = {} as PageRef;

    getControlForm(controlID: string): FormControl {
        return this.controlFormMap.get(controlID)!;
    }

    getPanelForm(panelID: string): FormGroup | FormArray {
        return this.panelFormMap.get(panelID)!;
    }

    getControl(controlID: string): WebControl {
        return this.controlMap.get(controlID)!;
    }

    getSection(sectionID: string): WebSection {
        return this.sectionMap.get(sectionID)!;
    }

    getPanel(panelID: string): WebPanel {
        return this.panelMap.get(panelID)!;
    }

    getPanelSections(panelID: string): WebSection[] {
        const sections: WebSection[] = [];
        const panel = this.panelMap.get(panelID);
        if(!panel) throw console.error('[DynamicFormSync] Panel not found');

        const sectionsOrdered = panel!.webSections ??
            panel!.webSections ? panel!.webSections.map(sec => sec.sectionID) : null;
        
        sectionsOrdered?.forEach(sec => {
            const section = this.sectionMap.get(sec);
            if(section) sections.push(section);
        });

        return sections;
    }

    getSectionControls(sectionID: string): WebControl[] {
        const controls: WebControl[] = [];

        const section = this.sectionMap.get(sectionID);
        if(!section) throw console.error('[DynamicFormSync] Section not found');

        const controlsOrdered = section.controls ?? 
            section.webControls ? section.webControls?.map(ctrl => ctrl.controlID) : null;

        controlsOrdered?.forEach(ctrl => {
            const control = this.controlMap.get(ctrl);
            if(control) controls.push(control);
        });

        return controls;
    }

    getPanelRef(panelID: string): PanelRef | undefined {
        return this.pageRef.panels.find((panel: PanelRef) => panel.panelID === panelID);
    }

    getSectionRef(panelID: string, sectionID: string): SectionRef | undefined {
        return this.getPanelRef(panelID)?.sections.find((sec: SectionRef) => sec.sectionID === sectionID);
    }
}