import { Component, computed, input } from '@angular/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatBadgeModule } from '@angular/material/badge';
import { NavConfig } from '../../../../models/nav.model';

@Component({
  selector: 'sl-nav',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.scss'
})
export class NavComponent {
  nav = input.required<NavConfig>();
  collapsed = input.required<boolean>();

  errorCount = computed<boolean>(()=> {
    if(this.nav().errorCount && this.nav().count) {
      return this.nav().count! >= this.nav()!.errorCount!;
    }
    return false;
  })

  warningCount = computed<boolean>(()=> {
    if(this.nav().warningCount && this.nav().count) {
      if(this.nav().errorCount && this.nav().count! >= this.nav()!.errorCount!) {
        return false;
      }
      else return this.nav().count! >= this.nav()!.warningCount!;
    }
    return false;
  })

  matBadgeColor = computed<string>(()=> {
    if(this.errorCount()) return 'var(--error-light)';
    else return 'var(--error-light)'
  })
}
