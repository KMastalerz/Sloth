import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, AuthStateService } from '@sloth-http';
import { LoginPageControls } from '@sloth-shared';
import { BasePage, BrandingSectionComponent, ButtonComponent, InputComponent, PasswordComponent } from '@sloth-ui';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [FormsModule, InputComponent, PasswordComponent, ButtonComponent, BrandingSectionComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);
  private router = inject(Router);
  
  loginInput = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Login));
  passwordInput = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Password));
  processButton = computed(()=> this.pageSync()?.getControlByID(LoginPageControls.Process));

  override ngOnInit() {
    super.ngOnInit();
  } 

  async onClick(): Promise<void> {      
    const command = {
      login: 'KRZMAS',
      password: 'master'
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

