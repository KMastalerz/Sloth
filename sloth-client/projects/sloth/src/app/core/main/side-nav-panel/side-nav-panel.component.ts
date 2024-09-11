import { ChangeDetectionStrategy, Component, computed, input, signal } from '@angular/core';
import { SideNavComponent, SideNavConfig } from '@sloth-ui';

@Component({
  selector: 'sl-side-nav-panel',
  standalone: true,
  imports: [SideNavComponent],
  templateUrl: './side-nav-panel.component.html',
  styleUrl: './side-nav-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SideNavPanelComponent {
  mainNavigations = input<SideNavConfig[]>([])
  userNavigations = input<SideNavConfig[]>([])
  settingsNavigations = input<SideNavConfig[]>([])

  protected collapsed = signal<boolean>(false);
  protected toggleIcon = computed<'chevron_right' | 'chevron_left'>(()=> this.collapsed() ? 'chevron_right' : 'chevron_left')
  protected hasMainNavigation = computed<boolean>(()=> this.mainNavigations() && this.mainNavigations().length > 0);
  protected hasUserNavigations = computed<boolean>(()=> this.userNavigations() && this.userNavigations().length > 0);
  protected hasSettingsNavigations = computed<boolean>(()=> this.settingsNavigations() && this.settingsNavigations().length > 0);

  protected onToggleExpander() {
    this.collapsed.set(!this.collapsed());
  }
  
}
