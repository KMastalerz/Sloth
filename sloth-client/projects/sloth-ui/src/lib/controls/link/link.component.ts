import { Component, model, signal } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink, RouterLinkActive } from '@angular/router';

import { BaseControl } from '../../engine/base/base-control/base-control.component';
import { IBaseControl } from '../../engine/base/base-control.interface';
import { IconNames } from '../../constants/icon.constants';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [MatTooltipModule, MatBadgeModule, RouterLink, RouterLinkActive],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl implements IBaseControl {
  setMetadata(): void {
    if (this.metaData()){
      this.icon.set(this.metaData().icon ?? undefined);
      this.type.set(this.metaData().type ?? IconNames.Default);
      this.counterFunc.set(this.metaData().counterFunc ?? undefined);
      this.warningCount.set(this.metaData().warningCount ?? undefined);
      this.errorCount.set(this.metaData().errorCount ?? undefined);
    }
  }

  icon = model<string | undefined>(undefined);
  type = model<string | undefined>(undefined);
  counterFunc = model<string | undefined>(undefined);
  warningCount = model<number | undefined>(undefined);
  errorCount = model<number | undefined>(undefined);

  count = signal<number>(0);

  // Todo: Implement the counter function
}
