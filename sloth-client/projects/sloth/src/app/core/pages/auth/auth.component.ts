import { Component, inject, input, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, AuthStateService, LoginCommand } from '@sloth-http';
import { Action, BrandingSectionComponent, DynamicGridComponent, DynamicPageSync } from '@sloth-ui';

@Component({
  selector: 'sl-auth',
  standalone: true,
  imports: [DynamicGridComponent, BrandingSectionComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent implements OnInit {
  pageSync = input.required<DynamicPageSync>();
  private router = inject(Router);
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);

  panelID = signal('login');

  ngOnInit(): void {
    // when auth page entered kill token 
    this.authStateService.clearAccessTokenResponse();
  }

  async onAction(action: Action): Promise<void> {
    const command = action.param as LoginCommand;
    
    const result = await this.authService.loginAsync(command);
    
    if (result.success) {
      this.authStateService.casheAccessTokenResponse(result.data);
      this.router.navigate(['main']);
    } else {
      // TO DO: Show error message
    }
  }
}

