import { Routes } from "@angular/router";
import { accessGuard } from "../../core/guards/access/access.guard";
import { DocumentationComponent } from "./documentation.component";
import { DocumentationListComponent } from "./documentation-list/documentation-list.component";
import { ArticleComponent } from "./documentation-list/article/article.component";
import { DiagramsComponent } from "./documentation-list/article/diagrams/diagrams.component";
import { UserStoriesComponent } from "./documentation-list/article/user-stories/user-stories.component";

export const documentationRoutes: Routes = [
    {
      path: '',
      component: DocumentationComponent, 
      canActivate: [accessGuard],
      children: [
        {
          path: '',
          redirectTo: 'list',
          pathMatch: 'full'
        },
        {
          path: 'list',
          component: DocumentationListComponent,
          canActivate: [accessGuard],
          children: [
            {
                path: 'list/:id',
                component: ArticleComponent,
                canActivate: [accessGuard],
                children: [
                    {
                        path: 'list/:id/diagrams',
                        component: DiagramsComponent,
                    },
                    {
                        path: 'list/:id/user-stories',
                        component: UserStoriesComponent,
                    }
                ]
              },
          ]
        },
      ]
    }
  ];