import { Routes } from '@angular/router';
import { pageResolver } from '@sloth-ui';
import { MainComponent } from './core/pages/main/main.component';
import { AuthComponent } from './core/pages/auth/auth.component';
import { authGuard } from './core/guards/auth/auth.guard';
import { NoServiceComponent } from './core/pages/no-service/no-service.component';
import { TestComponent } from './core/pages/test/test.component';
export const routes: Routes = [
    {   
        path: '', 
        redirectTo: 'test', 
        pathMatch: 'full'
    },
    {
        path: 'no-service',
        component: NoServiceComponent
    },   
    {   
        path: 'test', 
        component: TestComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
    {   
        path: 'auth', 
        component: AuthComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
    {   
        path: 'main', 
        component: MainComponent,
        // canActivate: [authGuard],  
        resolve: {
            pageSync: pageResolver
        }
        // children: [
        //     {
        //         // path: 'project-management',
        //         // component: ProjectManagementComponent,
        //         // resolve: {
        //         //     pageSync: pageResolver
        //         // },
        //         // children: projectRoutes
        //     }
        // ]
    },
];
