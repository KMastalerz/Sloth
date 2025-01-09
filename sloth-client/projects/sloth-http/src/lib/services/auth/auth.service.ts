import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { LoginParam } from '../../dto/auth/login.param';
import { AccessTokenResponse } from '../../dto/auth/access-token-response';
import { RefreshTokenParam } from '../../dto/auth/refresh-token.param';

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
    return this.get<AccessTokenResponse>("RefreshToken", command);
  }
}
