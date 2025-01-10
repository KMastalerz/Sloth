import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccessTokenResponse } from 'sloth-utilities';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { LoginParam } from '../../dto/auth/params/login.param';
import { RefreshTokenParam } from '../../dto/auth/params/refresh-token.param';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  constructor() { 
    super("Auth")
  }

  async loginAsync(command: LoginParam): Promise<ServiceReturnValue<AccessTokenResponse>> {
    return await this.postAsync<AccessTokenResponse>("Login", command);
  }

  refreshToken(command: RefreshTokenParam): Observable<ServiceReturnValue<AccessTokenResponse>> {
    return this.post<AccessTokenResponse>("RefreshToken", command);
  }
}
