import { Component } from '@angular/core';
import { SideNavComponent } from './side-nav/side-nav.component';
import { BasePage } from '@sloth-ui';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavComponent, RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {
  
}

