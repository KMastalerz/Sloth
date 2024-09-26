import { Component } from '@angular/core';
import { SideNavComponent } from './side-nav/side-nav.component';
import { BasePage } from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {
  
}

