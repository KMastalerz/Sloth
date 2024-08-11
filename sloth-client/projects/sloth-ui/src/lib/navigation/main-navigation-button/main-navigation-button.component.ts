import { Component, inject, signal } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { BaseControlComponent } from '../../base/base-control/base-control.component';
import { BaseControl } from '../../base/base-interface/base-control';
import { IconLibraryService } from '../../services/icon-library/icon-library.service';
import { IconDefinition, IconName, IconPrefix } from '@fortawesome/fontawesome-svg-core';

@Component({
  selector: 'sl-main-navigation-button',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './main-navigation-button.component.html',
  styleUrl: './main-navigation-button.component.scss'
})
export class MainNavigationButtonComponent extends BaseControlComponent implements BaseControl {
  private iconServices = inject(IconLibraryService);
  protected iconDefinition = signal<IconDefinition>(this.iconServices.getIcon('fas', 'plus'));
  protected text = signal<string>('');

  ngOnInit(): void {
    this.readConfig();
  } 

  readConfig(): void {
    //read configuration input.
    const icon = this.iconServices.getIcon(
      <IconPrefix>this.config().iconStyle,
      <IconName>this.config().icon);
    this.iconDefinition.set(icon);
    this.text.set(this.config().text);
  } 
}
