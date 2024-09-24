import { Injectable } from '@angular/core';

import { BaseService } from '../base/base-service.class';
import { SlothControllers } from '../../constants/controller.constants';
import { UIElementsActions } from '../../constants/action.constants';
import { GetWebPageQuery } from '../../queries/ui-service/get-web-page.query';
import { ServiceReturnValue } from '../../models/base/service-return.model';
import { WebPage } from '../../models/ui-service/page.model';


@Injectable({
  providedIn: 'root'
})
export class UIService extends BaseService {
  constructor(){
    super(SlothControllers.UIElements);
  }

  async getWebPageAsync(pageID: string): Promise<ServiceReturnValue<WebPage>> {
    switch (pageID) {
      case 'main':
        return await this.getMainWebPageAsync();
      case 'login':
        return await this.getLoginWebPageAsync();
      default:
        const command = {PageID: pageID} as GetWebPageQuery;
        return await this.get<WebPage>(UIElementsActions.GetWebPage, command);
    }
  }

  async getMainWebPageAsync(): Promise<ServiceReturnValue<WebPage>> {
    return await this.get<WebPage>(UIElementsActions.GetMainWebPage);
  }

  async getLoginWebPageAsync(): Promise<ServiceReturnValue<WebPage>> {
    return await this.get<WebPage>(UIElementsActions.GetLoginWebPage);
  }
}
