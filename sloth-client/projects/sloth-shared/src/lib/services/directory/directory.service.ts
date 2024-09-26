import { Injectable } from '@angular/core';
import { Routes } from '../../constants/routes.constants';
import { Pages } from '../../constants/pages.constants';

@Injectable({
  providedIn: 'root'
})
export class DirectoryService {
  routeMap: Map<string, string> = new Map<string, string>();

  private registerRoute(route: string, pageID: string) {
    this.routeMap.set(route, pageID);
  }

  getPageID(route: string): string {
    return this.routeMap.get(route) || '';
  }

  constructor() { 
    this.registerRoutes();
  }

  registerRoutes() {
    this.registerRoute(Routes.Auth, Pages.Auth);
    this.registerRoute(Routes.Main, Pages.Main);
  }

}
