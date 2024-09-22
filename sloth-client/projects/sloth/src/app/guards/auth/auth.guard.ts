import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { StorageService, StorageType } from '@sloth-util';

export const authGuard: CanActivateFn = (route, state) => {
  const storageService = inject(StorageService);
  const router = inject(Router);

  // Check if user is authenticated by looking for an auth token
  const authToken = storageService.getItem('AuthToken', StorageType.COOKIE);
  
  if (!authToken) {
    // If token is not present, redirect to login
    router.navigate(['auth']);
    return false;
  }

  // If token is present, allow access
  return true;
};
