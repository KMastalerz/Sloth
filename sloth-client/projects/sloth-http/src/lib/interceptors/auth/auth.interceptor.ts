import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

import { CookieKeys, StorageService, StorageType } from '@sloth-util';

import { AuthService } from '../../services/auth/auth.service';
import { catchError, switchMap, throwError } from 'rxjs';
import { AccessTokenResponse } from '../../models/auth/access-token-response.model';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const storageService = inject(StorageService);
  const authService = inject(AuthService);
  const router = inject(Router);
  // Get token from storage
  const token = storageService.getItem(CookieKeys.AuthToken, StorageType.COOKIE);
  
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
        return throwError(() => new Error('Token not found'));
      }

      // Handle token expiration (status 401)
      if (error.status === 401 && error.error.message === 'TokenExpired') {
        const refreshToken = storageService.getItem(CookieKeys.RefreshToken, StorageType.COOKIE);

        // If no refresh token, navigate to login
        if (!refreshToken) {
          router.navigate(['auth']);
          return throwError(() => new Error('Refresh token not found'));
        }

        // Call API to refresh token
        return authService.refreshToken(refreshToken).pipe(
          switchMap((response: AccessTokenResponse) => {
            // Store new tokens
            storageService.setItem(CookieKeys.AuthToken, response.accessToken, StorageType.COOKIE);
            storageService.setItem(CookieKeys.RefreshToken, response.refreshToken, StorageType.COOKIE);

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
            return throwError(() => new Error('Refresh token failed'));
          })
        );
      }
      // If any other error occurs, propagate it
      return throwError(() => error);
    })
  );
};
