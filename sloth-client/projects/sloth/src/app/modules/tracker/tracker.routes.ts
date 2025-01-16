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
      canActivate: [accessGuard]
    },
    {
      path: 'bugs/:bugID',
      component: BugComponent,
      canActivate: [accessGuard]  
    },
    {
      path: 'projects',
      component: ProjectsComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'projects/:projectID',
      component: ProjectComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'queries',
      component: QueriesComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'queries/:queryID',
      component: QueryComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'tasks',
      component: TasksComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'task/:taskID',
      component: TaskComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'tests',
      component: TestsComponent,
      canActivate: [accessGuard]
    },
    {
      path: 'test/:testID',
      component: TestComponent,
      canActivate: [accessGuard]
    }
  ];