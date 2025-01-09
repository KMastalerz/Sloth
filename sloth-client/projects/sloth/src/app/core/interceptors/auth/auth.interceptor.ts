import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  // const authStateService = inject(AuthStateService);
  // const authService = inject(AuthService);
  const router = inject(Router);
  return next(req);
};
