import { Routes } from "@angular/router";
import { accessGuard } from "../../core/guards/access/access.guard";
import { BugsComponent } from "./bugs/bugs.component";
import { BugComponent } from "./bugs/bug/bug.component";
import { ProjectsComponent } from "./projects/projects.component";
import { ProjectComponent } from "./projects/project/project.component";
import { QueriesComponent } from "./queries/queries.component";
import { QueryComponent } from "./queries/query/query.component";
import { TasksComponent } from "./tasks/tasks.component";
import { TaskComponent } from "./tasks/task/task.component";
import { TestsComponent } from "./tests/tests.component";
import { TestComponent } from "./tests/test/test.component";

export const trackerRoutes: Routes = [
    {
        path: '',
        redirectTo: 'bugs',
        pathMatch: 'full'
    },
    {
      path: 'bugs',
      component: BugsComponent,
      canActivate: [accessGuard],
      children: [
        {
            path: 'bugs/:id',
            component: BugComponent,
            canActivate: [accessGuard]  
        }
      ]
    },
    {
      path: 'projects',
      component: ProjectsComponent,
      canActivate: [accessGuard],
      children: [
        {
            path: 'projects/:id',
            component: ProjectComponent,
            canActivate: [accessGuard]
        }
      ]
    },
    {
      path: 'queries',
      component: QueriesComponent,
      canActivate: [accessGuard],
      children: [
        {
          path: 'queries/:id',
          component: QueryComponent,
          canActivate: [accessGuard]
        }
      ]
    },
    {
      path: 'tasks',
      component: TasksComponent,
      canActivate: [accessGuard],
      children: [
        {
          path: 'tasks/:id',
          component: TaskComponent,
          canActivate: [accessGuard]
        }
      ]
    },
    {
      path: 'tests',
      component: TestsComponent,
      canActivate: [accessGuard],
      children: [
        {
          path: 'tests/:id',
          component: TestComponent,
          canActivate: [accessGuard]
        }
      ]
    }
  ];