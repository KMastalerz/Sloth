import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from '../base/base-service.class';
import { SlothControllers } from '../../constants/controller.constants';
import { UIElementsActions } from '../../constants/action.constants';
import { GetWebPageQuery } from '../../queries/ui-service/get-web-page.query';
import { WebPage } from '../../models/ui-service/page.model';
import { ServiceReturnValue } from '../../models/base/service-return.model';


@Injectable({
  providedIn: 'root'
})
export class UIService extends BaseService {
  constructor(){
    super(SlothControllers.UIElements);
  }

  async getWebPage(pageID: string): Promise<ServiceReturnValue<WebPage>> {
    const command = {PageID: pageID} as GetWebPageQuery;
    return await this.get<WebPage>(UIElementsActions.GetWebPage, command);
  }

  async getMainWebPage(): Promise<ServiceReturnValue<WebPage>> {
    return await this.get<WebPage>(UIElementsActions.GetMainWebPage);
  }

  async getLoginWebPage(): Promise<ServiceReturnValue<WebPage>> {
    return await this.get<WebPage>(UIElementsActions.GetLoginWebPage);
  }
}
