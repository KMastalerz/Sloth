import { inject, Injectable } from '@angular/core';

import { DynamicDirectoryService } from '../dynamic-directory/dynamic-directory.service';
import { LinkComponent } from '../../../controls/link/link.component';
import { ToggleIconComponent } from '../../../controls/toggle-icon/toggle-icon.component';
import { PasswordComponent } from '../../../controls/password/password.component';
import { ButtonComponent } from '../../../controls/button/button.component';
import { InputComponent } from '../../../controls/input/input.component';

import { SideNavPanelComponent } from '../../../panels/side-nav-panel/side-nav-panel.component';
import { LoginPanelComponent } from '../../../panels/login-panel/login-panel.component';
import { PanelFormType, PanelType } from '@sloth-shared';

@Injectable({
  providedIn: 'root'
})
export class DynamicRegistrationService {
  dirService = inject(DynamicDirectoryService);

  registerElements(): void{
    this.registerControls();
    this.registerPanels();
    this.registerPanelTypes();
    this.registerRoutes();
  }

  private registerControls(): void{
    this.dirService.registerControl('Button', ButtonComponent);
    this.dirService.registerControl('Input', InputComponent);
    this.dirService.registerControl('Link', LinkComponent);
    this.dirService.registerControl('Password', PasswordComponent);
    this.dirService.registerControl('ToggleIcon', ToggleIconComponent);
  }

  private registerPanels(): void{
    this.dirService.registerPanel(PanelType.SideNav, SideNavPanelComponent);
    this.dirService.registerPanel(PanelType.Login, LoginPanelComponent);
  }

  private registerPanelTypes(): void{
    this.dirService.registerPanelType(PanelType.SideNav, PanelFormType.Object);
    this.dirService.registerPanelType(PanelType.Login, PanelFormType.Object);
  }

  private registerRoutes(): void{
    this.dirService.registerRoute('auth', 'login');
    this.dirService.registerRoute('sloth', 'main');
  }
}
