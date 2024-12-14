import { WebControl, WebPage, WebPanel } from "@sloth-http";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { inject } from "@angular/core";
import { JsonService, ListService } from "@sloth-shared";
import { Validation } from "../models/validation.type";

export class DynamicPageSync {
    private formBuilder = inject(FormBuilder);
    private listUtil = inject(ListService);
    private jsonUtil = inject(JsonService);

    private webControlMap: Map<string, WebControl> = new Map<string, WebControl>();
    private webPanelMap: Map<string, WebPanel> = new Map<string, WebPanel>();

    pageConfig: WebPage = {} as WebPage;
    pageForm: FormGroup = {} as FormGroup;
    pageInstance: any;

    initialize(): void {
        this.buildForm();
    }

    private buildForm(): void {
        const formGroup = this.formBuilder.group({});
        const orderedPanels = this.pageConfig.panels?.split(',') || [];

        orderedPanels.forEach(panelID => {

            const panelConfig = this.pageConfig.webPanels?.find(panel => panel.panelID === panelID.trim())!;

            this.webPanelMap.set(panelConfig.panelID, panelConfig);

            const orderedSections = panelConfig.sections?.split(',') || [];

            orderedSections.forEach(sectionID => { 

                const sectionConfig = panelConfig.webSections?.find(section => section.sectionID === sectionID.trim())!;

                const orderedControls = sectionConfig.controls?.split(',') || [];

                orderedControls.forEach(controlID => {

                    const controlConfig = sectionConfig.webControls?.find(control => control.controlID === controlID.trim())!;

                    this.webControlMap.set(`${panelConfig.panelID}_${sectionConfig.sectionID}_${controlConfig.controlID}`, controlConfig);
                });
            });
        });

        this.pageForm = formGroup;
    }
    
    private buildControl(control: WebControl): FormControl { 
        if(control.validation) {
            const validation: Validation = this.jsonUtil.tryParse(control.validation);
            
        }

        return new FormControl();
    }

    populateData(data: any, panelID: string): void {

    }
}