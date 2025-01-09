import { Routes } from "@angular/router";
import { accessGuard } from "../../core/guards/access/access.guard";
import { TimeSheetsComponent } from "../admin/users/user/time-sheets/time-sheets.component";
import { CalendarComponent } from "./calendar.component";
import { TimeSheetComponent } from "./time-sheet/time-sheet.component";
import { ReleasesComponent } from "./releases/releases.component";

export const calendarRoutes: Routes = [
    {
      path: '',
      component: CalendarComponent, 
      canActivate: [accessGuard], 
      children: [
        {
          path: '',
          redirectTo: 'time-sheets',
          pathMatch: 'full'
        },
        {
          path: 'time-sheets',
          component: TimeSheetsComponent,
          canActivate: [accessGuard] ,
          children: [
            {
              path: 'time-sheets/:id',
              component: TimeSheetComponent,
              canActivate: [accessGuard] 
            }
          ]
        },
        {
          path: 'releases',
          component: ReleasesComponent,
          canActivate: [accessGuard]
        }
      ]
    },
  ];