import { Component, input, output } from '@angular/core';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'sl-section',
  imports: [MatIcon],
  templateUrl: './section.component.html',
  styleUrl: './section.component.scss'
})
export class SectionComponent {
  header = input<string>();
  actionButtonIcon = input<string>();
  hasActionButton = input<boolean>(false);
  action = output();

  onAction(): void {
    this.action.emit();
  }
}
