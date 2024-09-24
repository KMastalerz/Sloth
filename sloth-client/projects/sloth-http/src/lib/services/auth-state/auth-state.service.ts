import { inject, Injectable } from '@angular/core';
import { CookieKeys, DateUtilityService, StorageService, StorageType } from '@sloth-shared';

import { AccessTokenResponse } from '../../models/auth/access-token-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private storageService = inject(StorageService);
  private dateUtility = inject(DateUtilityService);

  get refreshToken(): string | null {
    return this.storageService.getItem(CookieKeys.RefreshToken, StorageType.COOKIE);
  }

  get token(): string | null {
    return this.storageService.getItem(CookieKeys.AuthToken, StorageType.COOKIE);
  }

  get expiresAt(): string | null {
    return this.storageService.getItem(CookieKeys.ExpiresAt, StorageType.COOKIE);
  }

  get refreshExpiresAt(): string | null {
    return this.storageService.getItem(CookieKeys.RefreshExpiresAt, StorageType.COOKIE);
  }

  get userGroup(): string | null {
    return this.storageService.getItem(CookieKeys.UserGroup, StorageType.COOKIE);
  }

  get userRole(): string | null {
    return this.storageService.getItem(CookieKeys.UserRole, StorageType.COOKIE);
  }

  get userName(): string | null {
    return this.storageService.getItem(CookieKeys.UserName, StorageType.COOKIE);
  }

  get isLoggedIn(): boolean | null {
    if(!this.token || this.token?.trim() === '') return false;

    if(!this.expiresAt || this.expiresAt?.trim() === '') return false;

    const expiresAt = this.dateUtility.toDateTime(this.expiresAt);

    return this.dateUtility.isBefore(expiresAt);
  }

  get isLoggedOut(): boolean | null {
    return !this.isLoggedIn;
  }

  casheAccessTokenResponse(tokenData: AccessTokenResponse) {
    this.clearAccessTokenResponse();
    this.storageService.setItem(CookieKeys.AuthToken, tokenData.accessToken, StorageType.COOKIE);
    this.storageService.setItem(CookieKeys.RefreshToken, tokenData.refreshToken, StorageType.COOKIE);
    this.storageService.setItem(CookieKeys.ExpiresAt, tokenData.expiresAt.toString(), StorageType.COOKIE);
    this.storageService.setItem(CookieKeys.UserGroup, tokenData.user.userGroup, StorageType.COOKIE);
    this.storageService.setItem(CookieKeys.UserRole, tokenData.user.userRole, StorageType.COOKIE);
    this.storageService.setItem(CookieKeys.UserName, tokenData.user.userName, StorageType.COOKIE);
  }

  clearAccessTokenResponse() {
    this.storageService.removeItem(CookieKeys.AuthToken, StorageType.COOKIE);
    this.storageService.removeItem(CookieKeys.RefreshToken, StorageType.COOKIE);
    this.storageService.removeItem(CookieKeys.ExpiresAt, StorageType.COOKIE);
    this.storageService.removeItem(CookieKeys.UserGroup, StorageType.COOKIE);
    this.storageService.removeItem(CookieKeys.UserRole, StorageType.COOKIE);
    this.storageService.removeItem(CookieKeys.UserName, StorageType.COOKIE);
  }
}

