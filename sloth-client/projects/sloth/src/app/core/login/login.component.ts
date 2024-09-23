import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, WebControl } from '@sloth-http';
import { BasePage, BrandingComponent, ButtonComponent, ControlType, InputComponent, PasswordComponent } from '@sloth-ui';
import { AuthStateService } from '@sloth-util';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [InputComponent, PasswordComponent, ButtonComponent, BrandingComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  constructor(){
    super();
    // when accessed destroy the token
    this.authStateService.clearAccessTokenResponse();
  }
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);
  private router = inject(Router);

  userControl = computed<WebControl>(() => this.controls().find(c => c.controlID == 'UserName' && c.controlType === ControlType.TextInput)!);
  passwordControl = computed<WebControl>(() => this.controls().find(c => c.controlID == 'Password' && c.controlType === ControlType.Password)!);
  loginButton = computed<WebControl>(() => this.controls().find(c => c.controlID == 'Login' && c.controlType === ControlType.Button)!);

  userName = signal<string>('');
  password = signal<string>('');

  async login(): Promise<void> {
    const command = {
      login: this.userName(),
      password: this.password()
    };
    
    const result = await this.authService.loginAsync(command);
    
    if (result.success) {
      // Cache token
      this.authStateService.casheAccessTokenResponse(result.data);
      // Redirect to home
      this.router.navigate(['sloth']);
    } else {
      // TO DO: Show error message
    }
  }
}
