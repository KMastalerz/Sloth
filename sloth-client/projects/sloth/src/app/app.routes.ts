import { Routes } from '@angular/router';
import { MainComponent } from './core/main/main.component';
import { LoginComponent } from './core/login/login.component';
import { authGuard } from './guards/auth/auth.guard';
import { dynamicFormSyncResolver } from '@sloth-ui';

export const routes: Routes = [
    {   
        path: '', 
        redirectTo: 'auth', 
        pathMatch: 'full'
    },
    {   
        path: 'sloth', 
        component: MainComponent,
        canActivate: [authGuard],  
        resolve: {
            formSync: dynamicFormSyncResolver
        }
    },
    {   
        path: 'auth', 
        component: LoginComponent,
        resolve: {
            formSync: dynamicFormSyncResolver
        }
    },
];
