import { Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { accessGuard } from "../../core/guards/access/access.guard";
import { ProductsComponent } from "./products/products.component";
import { ProductComponent } from "./products/product/product.component";
import { ReleasesComponent } from "../calendar/releases/releases.component";
import { ProjectsComponent } from "../tracker/projects/projects.component";
import { ProjectComponent } from "../tracker/projects/project/project.component";
import { SlasComponent } from "./slas/slas.component";
import { SlaComponent } from "./slas/sla/sla.component";
import { UsersComponent } from "./users/users.component";
import { UserComponent } from "./users/user/user.component";
import { TimeSheetsComponent } from "./users/user/time-sheets/time-sheets.component";
import { StatisticsComponent } from "./users/user/statistics/statistics.component";

export const adminRoutes: Routes = [
    {
      path: '',
      component: AdminComponent, 
      canActivate: [accessGuard], 
      children: [
        {
          path: '', 
          redirectTo: 'products', 
          pathMatch: 'full'
        },
        {
          path: 'products',
          component: ProductsComponent,
          canActivate: [accessGuard],
          children: [
            {
                path: 'products/:id',
                component: ProductComponent,
                canActivate: [accessGuard],
                children: [
                    {
                        path: 'products/:id/releases',
                        component: ReleasesComponent
                    },
                    {
                        path: 'products/:id/projects',
                        component: ProjectsComponent,
                        children: [
                            {
                                path: 'products/:id/projects/:projectId',
                                component: ProjectComponent
                            },
                        ]
                    },
                ]
            }
          ]
        },
        {
          path: 'slas',
          component: SlasComponent,
          canActivate: [accessGuard],
          children: [
            {
                path: 'slas/:id',
                component: SlaComponent
            },
          ]
        },
        {
          path: 'users',
          component: UsersComponent,
          canActivate: [accessGuard],
          children: [
            {
                path: 'users/:id',
                component: UserComponent,
                canActivate: [accessGuard],
                children: [
                    {
                        path: 'users/:id/time-sheets',
                        component: TimeSheetsComponent
                    },        
                    {
                        path: 'users/:id/statistics',
                        component: StatisticsComponent
                    }
                ]
            }
          ]
        },
      ]
    }
  ];