import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { AccessTokenResponse } from '@sloth-util';

import { BaseService } from '../base/base-service.class';
import { SlothControllers } from '../../constants/controller.constants';
import { AuthActions } from '../../constants/action.constants';
import { ServiceReturnValue } from '../../models/base/service-return.model';
import { LoginCommand, RefreshTokenCommand} from '../../models/auth/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  constructor(){
    super(SlothControllers.Auth);
  }

  async loginAsync(command: LoginCommand): Promise<ServiceReturnValue<AccessTokenResponse>> {
    return await this.post<AccessTokenResponse>(AuthActions.Login, command);
  }

  refreshToken(refreshToken: RefreshTokenCommand): Observable<AccessTokenResponse> {
    return this.getRaw<AccessTokenResponse>(AuthActions.RefreshToken);
  }
}
