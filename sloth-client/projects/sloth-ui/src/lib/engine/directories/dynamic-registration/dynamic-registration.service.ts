import { inject, Injectable } from '@angular/core';

import { DynamicDirectoryService } from '../dynamic-directory/dynamic-directory.service';

import { LinkComponent } from '../../../controls/link/link.component';
import { ToggleIconComponent } from '../../../controls/toggle-icon/toggle-icon.component';
import { PasswordComponent } from '../../../controls/password/password.component';
import { ButtonComponent } from '../../../controls/button/button.component';
import { InputComponent } from '../../../controls/input/input.component';

import { SideNavPanelComponent } from '../../../panels/side-nav-panel/side-nav-panel.component';
import { SimplePanelComponent } from '../../../panels/simple-panel/simple-panel.component';

import { SimpleSectionComponent } from '../../../sections/simple-section/simple-section.component';
import { BrandingSectionComponent } from '../../../sections/branding-section/branding-section.component';

import { ControlType, PanelFormType, PanelType, SectionType } from '../../../constants/ui.constants';

@Injectable({
  providedIn: 'root'
})
export class DynamicRegistrationService {
  dirService = inject(DynamicDirectoryService);

  registerElements(): void{
    this.registerControls();
    this.registerSections();
    this.registerPanels();
    this.registerPanelTypes();
    this.registerRoutes();
  }

  private registerControls(): void{
    this.dirService.registerControl(ControlType.Button, ButtonComponent);
    this.dirService.registerControl(ControlType.Input, InputComponent);
    this.dirService.registerControl(ControlType.Link, LinkComponent);
    this.dirService.registerControl(ControlType.Password, PasswordComponent);
    this.dirService.registerControl(ControlType.ToggleIcon, ToggleIconComponent);
  }

  private registerSections(): void{
    this.dirService.registerSection(SectionType.Branding, BrandingSectionComponent);
    this.dirService.registerSection(SectionType.Simple, SimpleSectionComponent);
  }

  private registerPanels(): void{
    this.dirService.registerPanel(PanelType.SideNav, SideNavPanelComponent);
    this.dirService.registerPanel(PanelType.Simple, SimplePanelComponent);
  }

  private registerPanelTypes(): void{
    this.dirService.registerPanelType(PanelType.SideNav, PanelFormType.Object);
    this.dirService.registerPanelType(PanelType.Simple, PanelFormType.Object);
  }

  private registerRoutes(): void{
    this.dirService.registerRoute('auth', 'login');
    this.dirService.registerRoute('sloth', 'main');
  }
}
