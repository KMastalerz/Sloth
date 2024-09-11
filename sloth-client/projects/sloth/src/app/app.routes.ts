import { ResolveFn, Routes } from '@angular/router';

import { MainComponent } from './core/main/main.component';
import { inject } from '@angular/core';
import { TempNavService } from './core/main/side-nav-panel/temp-nav.service';
import { SideNavConfig } from '@sloth-ui';

export const resolveMainNavigation: ResolveFn<SideNavConfig[]> = (activedRoute, routeState) => {
   const services = inject(TempNavService);

   return services.mainGroup();
}
export const resolveUserNavigation: ResolveFn<SideNavConfig[]> = (activedRoute, routeState) => {
    const services = inject(TempNavService);
 
    return services.userGroup();
 }
 export const resolveInfoNavigation: ResolveFn<SideNavConfig[]> = (activedRoute, routeState) => {
    const services = inject(TempNavService);
 
    return services.infoGroup();
 }

export const routes: Routes = [
    {   
        path: '', 
        component: MainComponent,
        resolve: {
            mainNavigations: resolveMainNavigation,
            userNavigations: resolveUserNavigation,
            settingsNavigations: resolveInfoNavigation
        }
    },
];
