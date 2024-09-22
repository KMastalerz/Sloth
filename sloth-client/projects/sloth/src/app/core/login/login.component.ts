import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, WebControl } from '@sloth-http';
import { BasePage, ButtonComponent, ControlType, InputComponent, PasswordComponent } from '@sloth-ui';
import { CookieKeys, StorageService, StorageType } from '@sloth-util';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [InputComponent, PasswordComponent, ButtonComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  private authService = inject(AuthService);
  private storageService = inject(StorageService);
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

    const result = await this.authService.login(command);
    if (result.success) {
      // cashe tokens
      this.storageService.setItem(CookieKeys.AuthToken, result.data.accessToken, StorageType.COOKIE);
      this.storageService.setItem(CookieKeys.RefreshToken, result.data.refreshToken, StorageType.COOKIE);
      // redirect to home
      this.router.navigate(['sloth']);
    } else {
      // Show error message
    }
  }
}
