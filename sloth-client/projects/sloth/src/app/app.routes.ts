import { Routes } from '@angular/router';
import { MainComponent } from './core/main/main.component';
import { LoginComponent } from './core/login/login.component';
import { authGuard } from './guards/auth/auth.guard';

export const routes: Routes = [
    {   
        path: '', 
        redirectTo: 'auth', 
        pathMatch: 'full'
    },
    // {   
    //     path: 'sloth', 
    //     component: MainComponent,
    //     canActivate: [authGuard],  
    //     resolve: {
    //     }
    // },
    {   
        path: 'auth', 
        component: LoginComponent,
        resolve: {
        }
    },
];
