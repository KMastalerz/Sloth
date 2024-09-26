import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService, AuthStateService } from '@sloth-http';

export const authGuard: CanActivateFn = (route, state) => {
  const authStateService = inject(AuthStateService);
  const authService = inject(AuthService);
  const router = inject(Router);
  
  if (!authStateService.isLoggedIn) {
    // if refresh token is not present, redirect to login
    const refreshToken = authStateService.refreshToken;
    const userName = authStateService.userName;
    if (!refreshToken || !userName) {
      router.navigate(['auth']);
      return false;
    }
    // If token is present, refresh it
    authService.refreshToken({refreshToken, userName}).subscribe({
      next: (result) => {
        authStateService.casheAccessTokenResponse(result);
        return true;
      },
      error: () => {
        router.navigate(['auth']);
        return false;
      }
    });
  } 
  // If token is present, allow access
  return true;
};
