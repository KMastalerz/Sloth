import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth/auth.guard';
import { accessGuard } from './core/guards/access/access.guard';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { MainPageComponent } from './core/components/main-page/main-page.component';

export const routes: Routes = [
      {   
        path: '', 
        redirectTo: 'sloth', 
        pathMatch: 'full'
      },          
      {
        path: 'auth',
        loadChildren: () => import('./core/core.routes').then(m => m.coreRoutes)
      },
      {
        path: 'sloth',
        component: MainPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'dashboard',
            pathMatch: 'full'
          },      
          {
            path: 'dashboard',
            component: DashboardComponent,
            canActivate: [authGuard]
          },
          {
            path: 'account',
            loadChildren: () => import('./modules/account/account.routes').then(m => m.accountRoutes),
            canActivate: [authGuard]
          },
          {
            path: 'admin',
            loadChildren: () => import('./modules/admin/admin.routes').then(m => m.adminRoutes),
            canActivate: [authGuard, accessGuard] 
          },
          {
            path: 'calendar',
            loadChildren: () => import('./modules/calendar/calendar.routes').then(m => m.calendarRoutes),
            canActivate: [authGuard, accessGuard]
          },
          {
            path: 'documentation',
            loadChildren: () => import('./modules/documentation/documentation.routes').then(m => m.documentationRoutes),
            canActivate: [authGuard, accessGuard]
          },
          {
            path: 'kanban',
            loadChildren: () => import('./modules/kanban/kanban.routes').then(m => m.kanbanRoutes),
            canActivate: [authGuard, accessGuard]
          },
          {
            path: 'tracker',
            loadChildren: () => import('./modules/tracker/tracker.routes').then(m => m.trackerRoutes),
            canActivate: [authGuard, accessGuard]
          }
        ]
      },
      { 
        path: '**', 
        redirectTo: 'auth/invalid-page' 
      } 
];