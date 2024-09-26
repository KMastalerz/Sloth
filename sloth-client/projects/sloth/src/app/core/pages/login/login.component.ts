import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, AuthStateService } from '@sloth-http';
import { BasePage } from '@sloth-ui';

@Component({
  selector: 'sl-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BasePage {
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);
  private router = inject(Router);
  
  username = signal<string>('');
  password = signal<string>('');

  override ngOnInit() {
    super.ngOnInit();
    console.log('LoginComponent initialized', this.pageSync());
  }

  async onClick(): Promise<void> {      
    const command = {
      login: this.username(),
      password: this.password()
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

