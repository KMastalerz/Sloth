import { Routes } from "@angular/router";
import { AccountComponent } from "./account.component";
import { AccountSettingsComponent } from "./account-settings/account-settings.component";

export const accountRoutes: Routes = [
    {
      path: '', 
      component: AccountComponent,
      children: [
        {
          path: '', 
          redirectTo: 'settings', 
          pathMatch: 'full'
        },
        {
          path: 'settings', 
          component: AccountSettingsComponent 
        }
      ]
    }
  ];