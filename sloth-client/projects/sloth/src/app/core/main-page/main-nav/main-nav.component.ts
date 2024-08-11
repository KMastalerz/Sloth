import { ChangeDetectionStrategy, Component, input } from '@angular/core';

import { MainNavigationButton, MainNavigationButtonComponent } from '@sloth-ui';

@Component({
  selector: 'sl-main-nav',
  standalone: true,
  imports: [MainNavigationButtonComponent],
  templateUrl: './main-nav.component.html',
  styleUrl: './main-nav.component.scss',
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainNavComponent {
  buttons = input.required<MainNavigationButton[]>();
}
