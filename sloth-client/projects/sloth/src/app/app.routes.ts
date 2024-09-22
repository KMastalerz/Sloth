import { Routes } from '@angular/router';
import { MainComponent } from './core/main/main.component';
import { LoginComponent } from './core/login/login.component';
import { authGuard } from './guards/auth/auth.guard';
import { getMainPageResolver } from './resolvers/main-page/get-main-page.resolver';
import { getLoginPageResolver } from './resolvers/login-page/get-login-page.resolver';

export const routes: Routes = [
    {   
        path: '', 
        redirectTo: 'sloth', 
        pathMatch: 'full'
    },
    {   
        path: 'sloth', 
        component: MainComponent,
        canActivate: [authGuard],  
        resolve: {
            page: getMainPageResolver,
        }
    },
    {   
        path: 'auth', 
        component: LoginComponent,
        resolve: {
            page: getLoginPageResolver,
        }
    },
];
