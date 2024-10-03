import { WebControl, WebPage, WebPanel, WebSection } from "@sloth-http";
import { BehaviorSubject } from "rxjs";
import { Action } from "./action";
import { FormArray, FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { inject } from "@angular/core";
import { ListUtilityService } from "@sloth-shared";

export class DynamicPageSync {
    private formBuilder = inject(FormBuilder);
    private listUtil = inject(ListUtilityService);

    private formPanelMap: Map<string, FormGroup | FormArray> = new Map<string, FormGroup | FormArray>();
    private formSectionMap: Map<string, FormGroup> = new Map<string, FormGroup>();
    private formControlMap: Map<string, FormControl> = new Map<string, FormControl>();
    private webControlMap: Map<string, WebControl> = new Map<string, WebControl>();
    private webSectionMap: Map<string, WebSection | undefined> = new Map<string, WebSection | undefined>();
    private webPanelMap: Map<string, WebPanel | undefined> = new Map<string, WebPanel | undefined>();

    pageConfig: WebPage = {} as WebPage;
    pageForm: FormGroup = {} as FormGroup;

    toParent: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);
    toChild: BehaviorSubject<Action | undefined> = new BehaviorSubject<Action | undefined>(undefined);
    
    getWebControlByIDs(panelID: string, sectionID: string, controlID: string): WebControl {
        return this.webControlMap.get(`${panelID}_${sectionID}_${controlID}`)!; 
    }

    getWebControlsByIDs(panelID: string, sectionID: string): WebControl[] {
        const section = this.webSectionMap.get(`${panelID}_${sectionID}`)!;
        return section.webControls!;
    }

    getWebSectionsByID(panelID: string): WebSection[] {
        const panel = this.webPanelMap.get(`${panelID}`)!;
        return panel.webSections!;
    }

    getWebPanelByID(panelID: string): WebPanel {
        return this.webPanelMap.get(panelID)!;
    }
    
    getPanelFormByID(panelID: string): FormArray | FormGroup {  
        return this.formPanelMap.get(panelID)! as FormArray | FormGroup;
    }

    getFormControl(control: WebControl): FormControl {
        return this.formControlMap.get(`${control.panelID}_${control.sectionID}_${control.controlID}`)!;
    }

    buildForm(): void {
        const formGroup = this.formBuilder.group({});

        const orderedPanels = this.pageConfig.panels.split(',');

        orderedPanels.forEach(panel => {
            const panelID = panel.trim();
            const panelConfig = this.pageConfig.webPanels?.find(panel => panel.panelID === panelID)!;
            const panelForm = this.buildPanel(panelConfig);
            this.formPanelMap.set(panelID, panelForm);
            this.webPanelMap.set(panelID, panelConfig);
            formGroup.addControl(panelID, panelForm);
        });

        this.pageForm = formGroup;
    }

    private buildPanel(panel: WebPanel): FormGroup | FormArray {
        let panelPart: FormGroup | FormArray;

        const arrayTypePanels = ["list", "array", "kanban", "board"];
        let panelType = 'form'

        if (this.listUtil.Contains(arrayTypePanels, panel.panelType)) {
            panelPart = this.formBuilder.array([]);
            panelType = 'array';
        } else {
            panelPart = this.formBuilder.group([]);
            panelType = 'form';
        }

        const orderedSections = panel.sections.split(',');

        orderedSections.forEach(section => {
            const sectionID = section.trim();
            const sectionConfig = panel.webSections?.find(section => section.sectionID === sectionID)!;
            const sectionForm = this.buildSection(sectionConfig);
            this.formSectionMap.set(sectionID, sectionForm);
            this.webSectionMap.set(`${panel.panelID}_${sectionID}`, sectionConfig);
            (panelPart as FormGroup).addControl(sectionID, sectionForm);
        });

        return panelPart;
    }

    private buildSection(section: WebSection): FormGroup {
        let sectionPart = this.formBuilder.group([]);

        const orderedControls = section.controls.split(',');
        const formControlTypeControls = ["input", "password", "list", "textArea"];
        
        orderedControls.forEach(control => {
            const controlID = control.trim();
            const controlConfig = section.webControls?.find(control => control.controlID === controlID)!;
            
            if(this.listUtil.Contains(formControlTypeControls, controlConfig.controlType)) {
                // create form control
                const controlForm = this.formBuilder.control(undefined);
                this.formControlMap.set(`${section.panelID}_${section.sectionID}_${controlID}`, controlForm);
                (sectionPart as FormGroup).addControl(controlID, controlForm);
            }

            this.webControlMap.set(`${section.panelID}_${section.sectionID}_${controlID}`, controlConfig);
        });

        return sectionPart;
    }
}