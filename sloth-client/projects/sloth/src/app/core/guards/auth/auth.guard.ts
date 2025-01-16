import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from 'sloth-http';
import { AuthStateService } from 'sloth-utilities';

export const authGuard: CanActivateFn = (route, state) => {
  const authStateService = inject(AuthStateService);
  const authService = inject(AuthService);
  const router = inject(Router);

  if(!authStateService.loggedIn()) {
    const refreshToken = authStateService.refreshToken;
    const userID = authStateService.user?.userID;

    // if there is not refresh token or username then redirect to login
    if (!refreshToken || !userID) {
      console.error('[authGuard]: not authorized');
      router.navigate(['auth', 'login']);
      return false;
    }

    console.log('[authGuard]: trying to refresh token');
    // try to call refresh token 
    authService.refreshToken({refreshToken, userID}).subscribe({
      next: (result) => {
        if(result.success) {
          authStateService.casheAccessTokenResponse(result.data);
          console.log('[authGuard]: token refreshed');
          return true;
        }
        else {
          console.error('[authGuard]: refresh token failed');
          router.navigate(['auth', 'login']);
          return false;
        }
      }
    });
    return false;
  }
  else return true;
};
