import { Component, inject, OnInit, signal } from '@angular/core';
import { AuthService, AuthStateService, LoginCommand } from '@sloth-http';
import { Action, BasePage, BrandingSectionComponent, DynamicFormComponent } from '@sloth-ui';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'sl-auth',
  standalone: true,
  imports: [DynamicFormComponent, BrandingSectionComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent extends BasePage implements OnInit {
  private authService = inject(AuthService);
  private authStateService = inject(AuthStateService);

  panelID = signal('login');

  ngOnInit(): void {
    // when auth page entered kill token 
    this.authStateService.clearAccessTokenResponse();

    this.pageSync()?.toParent.pipe(untilDestroyed(this)).subscribe(action => {
      if(action)
        this.onAction(action);
    });
  }

  async onAction(action: Action): Promise<void> {
    const command = action.param.value as LoginCommand;
    
    const result = await this.authService.loginAsync(command);
    
    if (result.success) {
      this.authStateService.casheAccessTokenResponse(result.data);
      this.router.navigate(['main']);
    } else {
      // TO DO: Show error message
    }
  }
}

