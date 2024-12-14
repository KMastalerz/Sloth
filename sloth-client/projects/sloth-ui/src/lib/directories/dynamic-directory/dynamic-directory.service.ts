import { Injectable } from '@angular/core';
import { ButtonComponent } from '../../controls/button/button.component';
import { LinkComponent } from '../../controls/link/link.component';
import { PasswordComponent } from '../../controls/password/password.component';

import { FormComponent } from '../../panels/form/form.component';
import { FlexFormComponent } from '../../panels/flex-form/flex-form.component';
import { GridFormComponent } from '../../panels/grid-form/grid-form.component';
import { HeaderComponent } from '../../panels/header/header.component';
import { SideNavComponent } from '../../panels/side-nav/side-nav.component';
import { TextAreaComponent } from '../../controls/text-area/text-area.component';
import { TextInputComponent } from '../../controls/text-input/text-input.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicDirectoryService {
  controlMap: Map<string, any> = new Map<string, any>();
  panelMap: Map<string, any> = new Map<string, any>();

  registerControl(conrtolType: string, control: any): void {
    this.controlMap.set(conrtolType, control);
  }

  registerPanel(panelType: string, panel: any): void {
    this.panelMap.set(panelType, panel);
  }

  getControl(conrtolType: string): any {
    return this.controlMap.get(conrtolType);
  }

  getPanel(panelType: string): any {
    return this.panelMap.get(panelType);
  }

  constructor() { 
    this.registerControl('button', ButtonComponent);
    this.registerControl('link', LinkComponent)
    this.registerControl('password', PasswordComponent);
    this.registerControl('textArea', TextAreaComponent);
    this.registerControl('textInput', TextInputComponent);

    this.registerPanel('form', FormComponent);
    this.registerPanel('flexForm', FlexFormComponent);
    this.registerPanel('gridForm', GridFormComponent);
    this.registerPanel('header', HeaderComponent);
    this.registerPanel('sideNav', SideNavComponent);
  }
}
