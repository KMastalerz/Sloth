import { ChangeDetectionStrategy, Component, OnInit, signal } from '@angular/core';

import { MainNavigationButton } from '@sloth-ui';
import { MAIN_BUTTONS } from './main-nav/main-buttons';
import { MainNavComponent } from './main-nav/main-nav.component';

@Component({
  selector: 'sl-main-page',
  standalone: true,
  imports: [MainNavComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainPageComponent implements OnInit {
  buttons = signal<MainNavigationButton[]>(MAIN_BUTTONS)

  ngOnInit(): void {
    // 
  }
}
