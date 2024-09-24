import { inject, Injectable } from '@angular/core';

import { DynamicDirectoryService } from '../dynamic-directory/dynamic-directory.service';
import { LinkComponent } from '../../controls/link/link.component';
import { ToggleIconComponent } from '../../controls/toggle-icon/toggle-icon.component';
import { PasswordComponent } from '../../controls/password/password.component';
import { ButtonComponent } from '../../controls/button/button.component';
import { InputComponent } from '../../controls/input/input.component';

import { SideNavPanelComponent } from '../../panels/side-nav-panel/side-nav-panel.component';
import { LoginPanelComponent } from '../../panels/login-panel/login-panel.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicRegistrationService {
  dirService = inject(DynamicDirectoryService);

  registerElements(): void{
    this.registerControls();
    this.registerPanels();
  }

  private registerControls(): void{
    this.dirService.registerControl('Button', ButtonComponent);
    this.dirService.registerControl('Input', InputComponent);
    this.dirService.registerControl('Link', LinkComponent);
    this.dirService.registerControl('Password', PasswordComponent);
    this.dirService.registerControl('ToggleIcon', ToggleIconComponent);
  }

  private registerPanels(): void{
    this.dirService.registerPanel('SideNav', SideNavPanelComponent);
    this.dirService.registerPanel('Login', LoginPanelComponent);
  }

  private registerRoutes(): void{
    this.dirService.registerRoute('auth', 'login');
    this.dirService.registerRoute('sloth', 'main');
  }
}
