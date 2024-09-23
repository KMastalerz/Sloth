import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';

import { AccessTokenResponse, AuthStateService} from '@sloth-util';

import { AuthService } from '../../services/auth/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authStateService = inject(AuthStateService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const token = authStateService.token;
  
  // Clone the request and add the Authorization header
  const clonedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
  
  // Proceed with the cloned request
  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      // If token not found, navigate to login page
      if (!token) {
        router.navigate(['auth']);
        return throwError(() => {
          console.error('Token not found');
        });
      }

      // Handle token expiration (status 401)
      if (error.status === 401 && error.error.message === 'TokenExpired') {
        const refreshToken = authStateService.refreshToken;
        const userName = authStateService.userName;

        // If no refresh token, navigate to login
        if (!refreshToken || !userName) {
          router.navigate(['auth']);
          return throwError(() => {
            console.error('Token or UserName not found');
          });
        }
        // Call API to refresh token
        return authService.refreshToken({refreshToken, userName}).pipe(
          switchMap((response: AccessTokenResponse) => {
            // Store new tokens
            authStateService.casheAccessTokenResponse(response);

            // Retry the original request with the new token
            const retryRequest = req.clone({
              setHeaders: {
                Authorization: `Bearer ${response.accessToken}`,
              },
            });
            return next(retryRequest);
          }),
          catchError(() => {
            // If refresh fails, redirect to login
            router.navigate(['auth']);
            return throwError(() => {
              console.error('Token not found');
            });
          })
        );
      }
      // If any other error occurs, propagate it
      return throwError(() => error);
    })
  );
};






  