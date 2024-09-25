import { inject, Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';

import { WebControl, WebPage, WebPanel } from '@sloth-http';
import { PanelFormType } from '@sloth-shared';

import { DynamicFormSync } from '../../engine/dynamic-form-sync';
import { DynamicDirectoryService } from '../directories/dynamic-directory/dynamic-directory.service';

@Injectable({
  providedIn: 'root'
})
export class FormBuilderService {
  private formBuilder = inject(FormBuilder);
  private dirService = inject(DynamicDirectoryService);

  buildFormSync(page: WebPage): DynamicFormSync {
    const formSync = new DynamicFormSync();
    formSync.pageConfig = page;

    const formGroup = this.formBuilder.group({});
    // Loop through panels to build the form
    page.webPanels?.forEach(panel => {
      const panelForm = this.buildPanel(panel, formSync);
      formGroup.addControl(panel.panelID, panelForm);
      formSync.panelMap.set(panel.panelID, panel);
      formSync.panelFormMap.set(panel.panelID, panelForm);
    });

    formSync.pageForm = formGroup;
    return formSync;
  }

  // Build panel form structure (FormGroup or FormArray based on panelType)
  private buildPanel(panel: WebPanel, formSync: DynamicFormSync): FormGroup | FormArray {
    const panelType = this.dirService.getPanelType(panel.panelType) ?? PanelFormType.Object; //defaults to object

    let panelPart: FormGroup | FormArray;

    if (panelType === PanelFormType.Object) {
      panelPart = this.formBuilder.group([]);
    } else {
      panelPart = this.formBuilder.array([]);
    }

    panel.webSections?.forEach(section => {
        formSync.sectionMap.set(section.sectionID, section);
        section.webControls?.forEach(control => {
          const controlForm = this.buildControl(control);
          if(panelType === PanelFormType.Object) {
            (panelPart as FormGroup).addControl(control.controlID, controlForm);
          } else {
            (panelPart as FormArray).push(controlForm);
          }
          formSync.controlMap.set(control.controlID, control);
          formSync.controlFormMap.set(control.controlID, controlForm);
        });
    });

    return panelPart;
  }

  // Build form control
  private buildControl(control: WebControl): FormControl {
    return this.formBuilder.control({});
  }

  // Update form with data 
  updateFormWithData(formGroup: FormGroup, data: any): void {
    if (!data || !formGroup) return;

    Object.keys(data).forEach(key => {
      if (formGroup.controls[key]) {
        formGroup.controls[key].setValue(data[key]);
      }
    });
  }

  // Add Control to form group dynamically
  addControl(formGroup: FormGroup, control: WebControl): void {
    formGroup.addControl(control.controlID, this.buildControl(control));
  }

  // Remove control from form group dynamically
  removeControl(formGroup: FormGroup, controlID: string): void {
    formGroup.removeControl(controlID);
  }
}
