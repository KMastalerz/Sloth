import { Routes } from '@angular/router';
import { MainComponent } from './core/pages/main/main.component';
import { LoginComponent } from './core/pages/login/login.component';
import { pageResolver } from './core/resolvers/page/page.resolver';
import { authGuard } from './core/guards/auth/auth.guard';

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
            pageSync: pageResolver
        }
    },
    {   
        path: 'auth', 
        component: LoginComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
];
