import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  // const authStateService = inject(AuthStateService);
  // const authService = inject(AuthService);
  const router = inject(Router);

  // if(!authStateService.isLoggedIn) {
  //   const refreshToken = authStateService.refreshToken;
  //   const userName = authStateService.userName;

  //   // if there is not refresh token or username then redirect to login
  //   if (!refreshToken || !userName) {
  //     router.navigate(['auth']);
  //     return false;
  //   }

  //   // try to call refresh token 
  //   authService.refreshToken({refreshToken, userName}).subscribe({
  //     next: (result) => {
  //       authStateService.casheAccessTokenResponse(result);
  //       return true;
  //     },
  //     error: () => {
  //       router.navigate(['auth']);
  //       return false;
  //     }
  //   });
  // }

  return true;
};
