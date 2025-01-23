import { Component, inject, input, model, Renderer2, signal } from '@angular/core';
import { MatDivider } from '@angular/material/divider';
import { GetUserCountersItem } from 'sloth-http';
import { moon, sun, NavigationLinkComponent, ToggleComponent } from 'sloth-ui';

@Component({
  selector: 'side-navigation',
  imports: [MatDivider, NavigationLinkComponent, ToggleComponent],
  templateUrl: './side-navigation.component.html',
  styleUrl: './side-navigation.component.scss'
})
export class SideNavigationComponent {
  userCounters = input<GetUserCountersItem | undefined>(undefined);
  private renderer = inject(Renderer2);
  isLight = model<boolean>(false);

  moon = signal(sun);
  sun = signal(moon);

  onThemeToggle() {
    this.renderer.setStyle(document.documentElement, 'color-scheme', this.isLight() ? 'light' : 'dark');
  }
}
