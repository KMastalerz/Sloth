import { Component } from '@angular/core';
import { SideNavComponent } from "./side-nav/side-nav.component";

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [SideNavComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {

}
