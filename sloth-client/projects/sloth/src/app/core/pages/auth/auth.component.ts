import { Component, computed, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService, AuthStateService } from '@sloth-http';
import { LoginPageControls } from '@sloth-shared';
import { BasePage, BrandingSectionComponent, ButtonComponent, InputComponent, PasswordComponent } from '@sloth-ui';

@Component({
  selector: 'sl-auth',
  standalone: true,
  imports: [FormsModule, InputComponent, PasswordComponent, ButtonComponent, BrandingSectionComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent extends BasePage {
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);

  loginInput = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Login));
  passwordInput = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Password));
  processButton = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Process));

  async onClick(): Promise<void> {      
    const command = {
      login: 'KRZMAS',
      password: 'master'
    };
    
    const result = await this.authService.loginAsync(command);
    
    if (result.success) {
      this.authStateService.casheAccessTokenResponse(result.data);
      this.router.navigate(['main']);
    } else {
      // TO DO: Show error message
    }
  }
}

