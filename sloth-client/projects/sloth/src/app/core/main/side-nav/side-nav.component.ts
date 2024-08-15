import { Component, computed, signal } from '@angular/core';
import { NavComponent } from "./nav/nav.component";
import { NavConfig } from '../../../models/nav.model';
import { HeaderComponent } from './header/header.component';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [NavComponent, HeaderComponent],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss'
})
export class SideNavComponent {
  collapsed = signal<boolean>(true);
  toggleIcon = computed<'chevron_right' | 'chevron_left'>(()=> this.collapsed() ? 'chevron_right' : 'chevron_left')

  /* main group */
  dashboard = signal<NavConfig>({
    text: 'Dashboard',
    icon: 'bar_chart_4_bars',
  });

  critical = signal<NavConfig>({
    text: 'Critical',
    icon: 'e911_emergency',
    count: 2
  })

  bugs = signal<NavConfig>({
    text: 'Bugs',
    icon: 'bug_report',
    count: 15,
    warningCount: 10,
    errorCount: 15
  })

  projects = signal<NavConfig>({
    text: 'Projects',
    icon: 'tactic',
    count: 2
  })

  tasks = signal<NavConfig>({
    text: 'Tasks',
    icon: 'task',
    count: 25
  })

  /* info group */
  documentation = signal<NavConfig>({
    text: 'Documentation',
    icon: 'book_5',
  })


  releases = signal<NavConfig>({
    text: 'Releases',
    icon: 'rocket_launch',
  })

  /* user group */
  messages = signal<NavConfig>({
    text: 'Messages',
    icon: 'mail',
    count: 97
  })

  timesheet = signal<NavConfig>({
    text: 'Timesheet',
    icon: 'calendar_month'
  })
  

  settings = signal<NavConfig>({
    text: 'Setttings',
    icon: 'settings'
  })

  toggle(): void {
    this.collapsed.set(!this.collapsed());
  }

}
