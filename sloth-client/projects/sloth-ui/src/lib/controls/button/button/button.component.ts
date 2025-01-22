import { Component, computed, input, output } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';


@Component({
  selector: 'sl-button',
  imports: [MatButtonModule, MatTooltipModule, MatBadgeModule],
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  type = input<'menu' | 'button' | 'submit' | 'reset'>('button');
  displayType = input<'add' | 'delete' | 'flat' | 'basic'>('basic');
  buttonClass = input<'regular' | 'error' | 'warning' | 'success'>('regular')
  badge = input<number | string | null>(null);
  isDisabled = input<boolean>(false);
  isHeader = input<boolean>(false);


  onClick = output();

  hideTooltip = computed(() => !this.tooltip());
  hideBadge = computed(() => !this.badge());
  isFlatButton = computed(()=> this.displayType() !== 'basic');

  onClickEmit(): void {
    this.onClick.emit();
  }
}
