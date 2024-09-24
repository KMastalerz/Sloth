import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@sloth-http';
import { BasePage } from '@sloth-ui';
import { AuthStateService } from '@sloth-shared';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  // constructor(){
  //   super();
  //   this.authStateService.clearAccessTokenResponse();
  // }

  // private authService = inject(AuthService);
  // private authStateService = inject(AuthStateService);
  // private router = inject(Router);

  async login(): Promise<void> {
    // const command = {
    //   login: this.userName(),
    //   password: this.password()
    // };
    
    // const result = await this.authService.loginAsync(command);
    
    // if (result.success) {
    //   this.authStateService.casheAccessTokenResponse(result.data);
    //   this.router.navigate(['sloth']);
    // } else {
    //   // TO DO: Show error message
    // }
  }
}
