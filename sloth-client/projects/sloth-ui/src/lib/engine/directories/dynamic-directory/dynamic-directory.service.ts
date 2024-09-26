import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DynamicDirectoryService {
  private controls: Map<string, any> = new Map<string, any>();
  private panels: Map<string, any> = new Map<string, any>();
  private sections: Map<string, any> = new Map<string, any>();

  private panelTypes: Map<string, string> = new Map<string, string>();
  private routes: Map<string, string> = new Map<string, string>();

  registerControl(controlType: string, control: any) {
    this.controls.set(controlType, control);
  }

  registerSection(sectionType: string, panel: any) {
    this.sections.set(sectionType, panel);
  }

  registerPanel(panelType: string, panel: any) {
    this.panels.set(panelType, panel);
  }

  registerPanelType(panelType: string, type: string) {
    this.panelTypes.set(panelType, type);
  }
  
  registerRoute(route: string, pageID: any) {
    this.routes.set(route, pageID);
  }

  getControl(controlType: string): any {
    return this.controls.get(controlType);
  }

  getSection(sectionType: string): any{
    return this.sections.get(sectionType);
  }

  getPanel(panelType: string): any {
    return this.panels.get(panelType);
  }

  getPanelType(panelType: string): string | undefined{
    return this.panelTypes.get(panelType);
  }

  getPageID(route: string): string | undefined{
    return this.routes.get(route);
  }
}
