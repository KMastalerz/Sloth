import { Routes } from "@angular/router";
import { pageResolver } from "../../core/resolvers/page/page.resolver";
import { ProjectAddComponent } from "./project-add/project-add.component";
import { ProjectOptionsComponent } from "./project-options/project-options.component";

export const projectRoutes: Routes = [
    {   
        path: 'project-add', 
        component: ProjectAddComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
    {   
        path: 'project-options', 
        component: ProjectOptionsComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
];
