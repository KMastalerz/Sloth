import { Routes } from '@angular/router';
import { MainComponent } from './core/pages/main/main.component';
import { AuthComponent } from './core/pages/auth/auth.component';
import { pageResolver } from './core/resolvers/page/page.resolver';
import { authGuard } from './core/guards/auth/auth.guard';
import { NoServiceComponent } from './core/pages/no-service/no-service.component';
import { ProductComponent } from './modules/product/product.component';
import { productRoutes } from './modules/product/product.routes';

export const routes: Routes = [
    {   
        path: '', 
        redirectTo: 'main', 
        pathMatch: 'full'
    },
    {
        path: 'no-service',
        component: NoServiceComponent
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
        canActivate: [authGuard],  
        resolve: {
            pageSync: pageResolver
        },
        children: [
            {
                path: 'product',
                component: ProductComponent,
                resolve: {
                    pageSync: pageResolver
                },
                children: productRoutes
            }
        ]
    },
];
