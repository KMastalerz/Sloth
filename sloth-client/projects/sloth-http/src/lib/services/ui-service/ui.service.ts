import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { BaseService } from '../base/base-service.class';
import { SlothControllers } from '../../constants/controller.constants';
import { UIElementsActions } from '../../constants/action.constants';
import { GetWebPageQuery } from '../../queries/ui-service/get-web-page.query';
import { WebPage } from '../../models/ui-service/page.model';


@Injectable({
  providedIn: 'root'
})
export class UIService extends BaseService {
  constructor(){
    super(SlothControllers.UIElements);
  }

  async getWebPage(pageID: string): Promise<WebPage | undefined> {
    const command = {PageID: pageID} as GetWebPageQuery;
    const res = await this.get<WebPage>(UIElementsActions.GetWebPage, command);
    return res;
  }

  async getMainWebPage(): Promise<WebPage | undefined> {
    const res = await this.get<WebPage>(UIElementsActions.GetMainWebPage);
    return res;
  }

  async getLoginWebPage(): Promise<WebPage | undefined> {
    const res = await this.get<WebPage>(UIElementsActions.GetLoginWebPage);
    return res;
  }
}
