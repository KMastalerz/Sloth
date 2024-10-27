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

    getWebControlsByID(panelID: string, controlID: string): WebControl[] {
        const panel = this.webPanelMap.get(`${panelID}`)!;
        return panel.webControls!;
    }

    getWebPanelByID(panelID: string): WebPanel {
        return this.webPanelMap.get(panelID)!;
    }

    getPanelForm(panelID: string): FormGroup | FormArray {
        const panel = this.pageForm.get(panelID);
        if(panel instanceof FormGroup) {
            return panel as FormGroup;
        } 
        return panel as FormArray;
    }

    getFormControl(panelID: string, controlID: string, index?: number | undefined): FormControl | FormGroup {
        // Retrieve the correct control (FormControl or FormGroup) based on its type and index if applicable
        const panelForm = this.getPanelForm(panelID);
        if (panelForm instanceof FormArray && typeof index !== 'undefined') {
            const group = panelForm.at(index) as FormGroup;
            return group.get(controlID) as FormControl | FormGroup;
        } else if (panelForm instanceof FormGroup) {
            return panelForm.get(controlID) as FormControl | FormGroup;
        }
        return null as any;
    }
    
    /**
     * Populates data at page, panel, or control level.
     * 
     * @param panelID Optional panel ID to populate a specific panel.
     * @param controlID Optional control ID to populate a specific control.
     * @param data The data to be set in the form.
     * @param index Optional index if the panel is a FormArray.
     */
    populateData(data: any, panelID?: string, index?: number, controlID?: string): void {
        if (!panelID) {
            // Populate at the page level (entire pageForm)
            if (this.pageForm) {
                this.pageForm.patchValue(data);
            }
        } else if (!controlID) {
            // Populate at the panel level
            const panelForm = this.getPanelForm(panelID);
            if (panelForm instanceof FormArray && Array.isArray(data)) {
                data.forEach((item, i) => {
                    let group = panelForm.at(i) as FormGroup;

                    // If FormGroup doesn't exist, create and add a new one
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
            // Populate at the control level
            const control = this.getFormControl(panelID, controlID, index);
            if (control) {
                control.setValue(data);
            }
        }
    }

    buildForm(): void {
        const formGroup = this.formBuilder.group({});

        const orderedPanels = this.pageConfig.panels.split(',');

        orderedPanels.forEach(panel => {
            const panelID = panel.trim();
            const panelConfig = this.pageConfig.webPanels?.find(panel => panel.panelID === panelID)!;
            const panelForm = this.buildPanel(panelConfig);
            this.webPanelMap.set(panelID, panelConfig);
            formGroup.addControl(panelID, panelForm);
        });

        this.pageForm = formGroup;
    }

    private buildPanel(panel: WebPanel): FormGroup | FormArray {
        let panelPart: FormGroup | FormArray;

        if (this.listUtil.Contains(StaticWebObjects.ArrayTypePanels, panel.panelType)) {
            panelPart = this.formBuilder.array([]);
            return panelPart; // Return FormArray as it will be populated later
        } else {
            panelPart = this.formBuilder.group([]);
        }

        const orderedControls = panel.controls.split(',');

        orderedControls.forEach(control => {
            const controlID = control.trim();
            const controlConfig = panel.webControls?.find(control => control.controlID === controlID)!;
            if (!controlConfig) {
                return;
            }
            
           // Check if control holds FormControl or FormGroup
           if (this.listUtil.Contains(StaticWebObjects.FormGroupTypeControls, controlConfig.controlType) && controlConfig.controls) {
                // Build FormGroup based on comma-delimited child controls
                const controlGroup = this.buildFormGroupFromControls(controlConfig.controls);
                (panelPart as FormGroup).addControl(controlID, controlGroup);
            } else if (this.listUtil.Contains(StaticWebObjects.FormControlTypeControls, controlConfig.controlType)) {
                const controlForm = this.formBuilder.control(undefined);
                (panelPart as FormGroup).addControl(controlID, controlForm);
            }

            this.webControlMap.set(`${panel.panelID}_${controlID}`, controlConfig);
        });

        return panelPart;
    }

    private buildFormGroupFromControls(childControls: string): FormGroup {
        const controlGroup = this.formBuilder.group({});
        const controlsArray = childControls.split(',');

        controlsArray.forEach(childControl => {
            const childControlID = childControl.trim();
            // Create a FormControl for each child control
            const controlForm = this.formBuilder.control(undefined);
            controlGroup.addControl(childControlID, controlForm);
        });

        return controlGroup;
    }

    private buildPanelFormGroup(panelID: string): FormGroup {
        // Create a FormGroup based on the panel's web controls
        const controlGroup = this.formBuilder.group({});
        const panelConfig = this.getWebPanelByID(panelID);
        panelConfig.webControls?.forEach(control => {
            const controlForm = this.formBuilder.control(undefined);
            controlGroup.addControl(control.controlID, controlForm);
        });
        return controlGroup;
    }
}