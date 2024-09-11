import { Injectable, signal } from '@angular/core';
import { SideNavConfig } from '@sloth-ui';

@Injectable({
  providedIn: 'root'
})
export class TempNavService {

  constructor() { }

  getNewMessageCount(): number {
    return Math.floor(Math.random() * (100 - 0 + 1)) + 0;
  }

  mainGroup = signal<SideNavConfig[]>([
      /* main group */
      {
        text: 'Dashboard',
        icon: 'bar_chart_4_bars',
      } as SideNavConfig,
      {
        text: 'Critical',
        icon: 'e911_emergency',
        count: 2
      } as SideNavConfig,
      {
        text: 'Bugs',
        icon: 'bug_report',
        count: 15,
        warningCount: 10,
        errorCount: 15
      } as SideNavConfig,
      {
        text: 'Projects',
        icon: 'tactic',
        count: 2
      } as SideNavConfig,
      {
        text: 'Tasks',
        icon: 'task',
        count: 25
      } as SideNavConfig,
  ])

  infoGroup = signal<SideNavConfig[]>([
    {
      text: 'Documentation',
      icon: 'book_5',
    } as SideNavConfig,
    {
      text: 'Releases',
      icon: 'rocket_launch',
    } as SideNavConfig,
  ]);

  userGroup = signal<SideNavConfig[]>([
    {
      text: 'Messages',
      icon: 'mail',
      count: 97,
      warningCount: 30,
      errorCount: 90
    } as SideNavConfig,
    {
      text: 'Timesheet',
      icon: 'calendar_month',
    } as SideNavConfig,
    {
      text: 'Setttings',
      icon: 'settings',
    } as SideNavConfig,
  ]);


}
