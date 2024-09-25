import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, AuthStateService } from '@sloth-http';
import { BasePage, DynamicPanelDirective } from '@sloth-ui';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [DynamicPanelDirective],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);
  private router = inject(Router);

  async login(): Promise<void> {
    const {Password, UserName} = this.pageForm().value.Login;
    console.log('[LoginComponent] Params:', Password, UserName);
    

    const command = {
      login: UserName,
      password: Password
    };
    
    const result = await this.authService.loginAsync(command);
    
    if (result.success) {
      this.authStateService.casheAccessTokenResponse(result.data);
      this.router.navigate(['sloth']);
    } else {
      // TO DO: Show error message
    }
  }
}
