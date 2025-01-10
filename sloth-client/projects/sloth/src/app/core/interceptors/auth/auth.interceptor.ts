import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, switchMap, throwError } from 'rxjs';
import { AuthService, ServiceReturnValue } from 'sloth-http';
import { AccessTokenResponse, AuthStateService } from 'sloth-utilities';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authStateService = inject(AuthStateService);
  const authService = inject(AuthService);
  const router = inject(Router);

  if (req.url.includes('/Login') || 
      req.url.includes('/RefreshToken')) {
    return next(req);
  }

  let token = authStateService.token;

  const clonedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });


  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      // on missing token go to login page
      if(!token) {
        router.navigate(['auth', 'login'])
        return throwError(()=>'Missing token.')
      }

      if(error.status === 401 && error.error.message === 'TokenExpired') {
        const refreshToken = authStateService.refreshToken;
        const userID = authStateService.user?.userID;

        // if user or refresh token is missing go to login page
        if(!refreshToken || !userID) {
          router.navigate(['auth', 'login'])
          return throwError(()=>'Missing refresh token.')
        }

        // call for refresh 
        return authService.refreshToken({refreshToken, userID}).pipe(
          switchMap((response: ServiceReturnValue<AccessTokenResponse>) => {
            if(response.success) {
              authStateService.casheAccessTokenResponse(response.data);
              token = authStateService.token;

              // retry the original request with the new token
              const retryRequest = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${token}`,
                },
              });
              return next(retryRequest);
            }
            // refresh failed, go to login page
            else {
              router.navigate(['auth', 'login'])
              return throwError(()=>'Token refresh failed (1).')
            }
          }),
          catchError(()=> {
            router.navigate(['auth', 'login'])
            return throwError(()=>'Token refresh failed (2).')
          })
        );
      }
      // If any other error occurs, propagate it 
      router.navigate(['auth', 'login'])
      return throwError(() => error);
    })
  );
};
