import { Component } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterOutlet } from '@angular/router';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { MainHeaderComponent } from './main-header/main-header.component';
import { NavigationHeaderComponent } from './navigation-header/navigation-header.component';

@Component({
  selector: 'sl-main-page',
  imports: [MatSidenavModule, MatIconModule, MatButtonModule, MatToolbarModule, MatMenuModule, MatBadgeModule, 
    MatDividerModule, RouterOutlet, SideNavigationComponent, MainHeaderComponent, NavigationHeaderComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {

}
