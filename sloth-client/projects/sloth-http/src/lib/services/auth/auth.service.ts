import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from '../base/base-service.class';
import { SlothControllers } from '../../constants/controller.constants';
import { LoginCommand } from '../../models/auth/login.model';
import { AuthActions } from '../../constants/action.constants';
import { AccessTokenResponse } from '../../models/auth/access-token-response.model';
import { ServiceReturnValue } from '../../models/base/service-return.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(){
    super(SlothControllers.Auth);
  }

  async login(command: LoginCommand): Promise<ServiceReturnValue<AccessTokenResponse>> {
    return await this.post<AccessTokenResponse>(AuthActions.Login, command);
  }

  refreshToken(refreshToken: string): Observable<AccessTokenResponse> {
    return this.getRaw<AccessTokenResponse>(AuthActions.RefreshToken);
  }
}
