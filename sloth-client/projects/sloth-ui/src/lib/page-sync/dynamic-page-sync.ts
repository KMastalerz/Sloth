import { WebControl, WebPage, WebPanel } from "@sloth-http";
import { BehaviorSubject } from "rxjs";
import { Action } from "./action";
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { inject } from "@angular/core";
import { ListService } from "@sloth-shared";
import { StaticWebObjects } from "../constants/page.constants";

export class DynamicPageSync {
    private formBuilder = inject(FormBuilder);
    private listUtil = inject(ListService);
    private webControlMap: Map<string, WebControl> = new Map<string, WebControl>();
    private webPanelMap: Map<string, WebPanel | undefined> = new Map<string, WebPanel | undefined>();

    pageConfig: WebPage = {} as WebPage;
    pageForm: FormGroup = {} as FormGroup;
    pageInstance: any;

    toParent: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);
    toChild: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);
    
    getWebControlByIDs(panelID: string, controlID: string): WebControl {
        return this.webControlMap.get(`${panelID}_${controlID}`)!; 
    }

    getWebPanelByID(panelID: string): WebPanel {
        return this.webPanelMap.get(panelID)!;
    }

    getPanelForm(panelID: string): FormGroup | FormArray {
        const panel = this.pageForm.get(panelID);
        if(panel instanceof FormGroup) {
            return panel;
        } 
        return panel as FormArray;
    }

    getFormControl(panelID: string, controlID: string, index?: number): FormControl | FormGroup {
        const panelForm = this.getPanelForm(panelID);
        if (panelForm instanceof FormArray && typeof index !== 'undefined') {
            const group = panelForm.at(index) as FormGroup;
            return group.get(controlID) as FormControl | FormGroup;
        } else if (panelForm instanceof FormGroup) {
            return panelForm.get(controlID) as FormControl | FormGroup;
        }
        return null as any;
    }
    
    populateData(data: any, panelID?: string, index?: number, controlID?: string): void {
        if (!panelID) {
            if (this.pageForm) {
                this.pageForm.patchValue(data);
            }
        } else if (!controlID) {
            const panelForm = this.getPanelForm(panelID);
            if (panelForm instanceof FormArray && Array.isArray(data)) {
                data.forEach((item, i) => {
                    let group = panelForm.at(i) as FormGroup;
                    if (!group) {
                        group = this.buildPanelFormGroup(panelID);
                        (panelForm as FormArray).insert(i, group);
                    }
                    group.patchValue(item);
                });
            } else if (panelForm instanceof FormGroup) {
                panelForm.patchValue(data);
            }
        } else {
            const control = this.getFormControl(panelID, controlID, index);
            if (control) {
                control.setValue(data);
            }
        }
    }

    buildForm(): void {
        const formGroup = this.formBuilder.group({});
        const orderedPanels = this.pageConfig.panels?.split(',') || [];

        orderedPanels.forEach(panelID => {
            const panelConfig = this.pageConfig.webPanels?.find(panel => panel.panelID === panelID.trim())!;
            const panelForm = this.buildPanel(panelConfig);
            this.webPanelMap.set(panelID.trim(), panelConfig);
            formGroup.addControl(panelID.trim(), panelForm);
        });

        this.pageForm = formGroup;
    }

    private buildPanel(panel: WebPanel): FormGroup | FormArray {
        let panelForm: FormGroup | FormArray;

        if (this.listUtil.Contains(StaticWebObjects.ArrayTypePanels, panel.panelType)) {
            panelForm = this.formBuilder.array([]);
        } else {
            panelForm = this.formBuilder.group({});
        }
        
        const orderedSections = panel.sections.split(',');

        orderedSections.forEach(sectionID => {
            sectionID = sectionID.trim();
            const sectionConfig = panel.webSections?.find(section => section.sectionID === sectionID.trim())!;
            const orderedControls = sectionConfig.controls?.split(',') || [];

            orderedControls.forEach(controlID => {
                controlID = controlID.trim();
                const controlConfig = sectionConfig.webControls?.find(control => control.controlID === controlID.trim());
                if (controlConfig) {
                    if (this.listUtil.Contains(StaticWebObjects.FormGroupTypeControls, controlConfig.controlType) && controlConfig.controls) {
                        const controlGroup = this.buildFormGroupFromControls(controlConfig.controls);
                        (panelForm as FormGroup).addControl(controlID.trim(), controlGroup);
                    } else if (this.listUtil.Contains(StaticWebObjects.FormControlTypeControls, controlConfig.controlType)) {
                        const controlForm = this.formBuilder.control(undefined);
                        (panelForm as FormGroup).addControl(controlID.trim(), controlForm);
                    }

                    this.webControlMap.set(`${panel.panelID}_${controlID.trim()}`, controlConfig);
                }
            });
        });

        return panelForm;
    }

    private buildFormGroupFromControls(childControls: string): FormGroup {
        const controlGroup = this.formBuilder.group({});
        const controlsArray = childControls.split(',');

        controlsArray.forEach(childControlID => {
            const controlForm = this.formBuilder.control(undefined);
            controlGroup.addControl(childControlID.trim(), controlForm);
        });

        return controlGroup;
    }

    private buildPanelFormGroup(panelID: string): FormGroup {
        const controlGroup = this.formBuilder.group({});
        const panelConfig = this.getWebPanelByID(panelID);
    
        // Iterate through each section in the panel
        panelConfig.webSections?.forEach(section => {
            section.webControls?.forEach(control => {
                // For each control in the section, create a FormControl and add it to the group
                const controlForm = this.formBuilder.control(undefined);
                controlGroup.addControl(control.controlID, controlForm);
                this.webControlMap.set(`${panelID}_${control.controlID}`, control);
            });
        });
    
        return controlGroup;
    }
}