import { inject, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouterService {
  router = inject(Router);
  activatedRoute = inject(ActivatedRoute);
  
  isCurrentComponent(target: any): boolean {
    let currentRoute = this.activatedRoute;

    while (currentRoute.firstChild) {
      currentRoute = currentRoute.firstChild;
    }

    const currentComponent = currentRoute.component;

    return currentComponent === target;
  }
  
  isComponentInRoute(target: any) {
    return this._isComponentInRoute(this.activatedRoute, target);
  }

  private _isComponentInRoute(route: ActivatedRoute, target: any): boolean {
    if (route.component === target) {
      return true;
    }

    return route.children.some(child => this._isComponentInRoute(child, target));
  }
}
