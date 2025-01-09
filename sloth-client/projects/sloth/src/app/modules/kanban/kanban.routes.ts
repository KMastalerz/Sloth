import { Routes } from "@angular/router";
import { KanbanComponent } from "./kanban.component";
import { accessGuard } from "../../core/guards/access/access.guard";

export const kanbanRoutes: Routes = [
    {
      path: '',
      component: KanbanComponent,  
      canActivate: [accessGuard]  
    }
  ];