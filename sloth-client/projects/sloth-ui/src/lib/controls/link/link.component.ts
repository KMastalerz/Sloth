import { Component, computed, input, signal } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatTooltip } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { BaseControl } from '../../base/base-control/base-control.component';

@Component({
  selector: 'sl-link',
  standalone: true,
  imports: [RouterLink, MatTooltip, MatBadge],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss'
})
export class LinkComponent extends BaseControl {
  collapsed = input<boolean>(false);
  
  color = computed<string | undefined>(()=>this.metaData()?.color ?? 'transparent');

  warningCount = computed<number | undefined>(()=>this.metaData()?.warningCount);
  errorCount = computed<number | undefined>(()=>this.metaData()?.errorCount);

  // TO DO: Implement, on error fallback to default
  checkResult = signal<string>('info');
  count = signal<number>(0);
}
