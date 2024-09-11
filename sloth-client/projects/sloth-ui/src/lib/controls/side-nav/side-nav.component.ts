import { ChangeDetectionStrategy, Component, computed, ElementRef, input, signal, viewChild, ViewEncapsulation } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'sl-side-nav',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule],
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SideNavComponent {
  collapsed = input.required<boolean>();
  text = input.required<string>();
  icon = input.required<string>();

  displayCounter = input<boolean>(false);
  count = input<number>(0);

  warningCount = input<number | null>(null);
  errorCount = input<number | null>(null);
  route = input<string | null>(null);

  type = computed<'info' | 'warn' | 'err'>(() => {

    if(this.errorCount()) {
      if (this.count() >= this.errorCount()!) 
        return 'err'
    }

    if(this.warningCount()) {
      if (this.count() >= this.warningCount()!)
         return 'warn'
    }

    return 'info';
  });

  hideBagde = computed(()=> this.count() && this.count() > 0 && !this.collapsed());
}
