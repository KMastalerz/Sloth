import { inject, Injectable } from '@angular/core';
import { CacheService } from '../cache/cache.service';
import { CookieKeys } from '../cache/cache.model';
import { User } from '../shared-models/auth/user.item';
import { AccessTokenResponse } from '../shared-models/auth/access-token-response.item';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private cacheService = inject(CacheService);

  get token(): string | null {
    return this.cacheService.readFromCookies(CookieKeys.AuthToken);
  }

  get refreshToken(): string | null {
    return this.cacheService.readFromCookies(CookieKeys.RefreshToken);
  }

  get expiresAt(): Date | null {
    return this.cacheService.readFromCookies<Date | null>(CookieKeys.ExpiresAt);
  }

  get refreshExpiresAt(): Date | null {
    return this.cacheService.readFromCookies<Date | null>(CookieKeys.RefreshExpiresAt);
  }

  get user() : User | null {
    return this.cacheService.readFromCookies<User | null>(CookieKeys.User);
  }

  public userInRole(role: string): boolean {
    return this.user?.userRoles.some(r => r.roleCode === role || r.roleName === role) ?? false;
  }

  public loggedIn(): boolean {
    if (this.token && this.expiresAt && new Date(this.expiresAt).getTime() > new Date().getTime()) return true 
    else return false;
  }

  casheAccessTokenResponse(value: AccessTokenResponse) {
    this.cacheService.saveToCookies(CookieKeys.AuthToken, value.accessToken);
    this.cacheService.saveToCookies(CookieKeys.RefreshToken, value.refreshToken);
    this.cacheService.saveToCookies(CookieKeys.ExpiresAt, value.expiresAt);
    this.cacheService.saveToCookies(CookieKeys.RefreshExpiresAt, value.refreshExpiresAt);
    this.cacheService.saveToCookies(CookieKeys.User, value.user);
  }

  clearAccessTokenResponse() {
    this.cacheService.removeFromCookies(CookieKeys.AuthToken);
    this.cacheService.removeFromCookies(CookieKeys.RefreshToken);
    this.cacheService.removeFromCookies(CookieKeys.ExpiresAt);
    this.cacheService.removeFromCookies(CookieKeys.RefreshExpiresAt);
    this.cacheService.removeFromCookies(CookieKeys.User);
  }
 }
