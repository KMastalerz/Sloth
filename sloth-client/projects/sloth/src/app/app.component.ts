import { Component, ViewEncapsulation } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { MatDividerModule } from '@angular/material/divider';
import { RouterOutlet } from '@angular/router';
import { SideNavigationComponent } from "./core/side-navigation/side-navigation.component";
import { MainHeaderComponent } from './core/main-header/main-header.component';
import { NavigationHeaderComponent } from "./core/navigation-header/navigation-header.component";
@Component({
    selector: 'app-root',
    imports: [MatSidenavModule, MatIconModule, MatButtonModule, MatToolbarModule, MatMenuModule, MatBadgeModule, 
        MatDividerModule, RouterOutlet, SideNavigationComponent, MainHeaderComponent, NavigationHeaderComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    encapsulation: ViewEncapsulation.None 
})
export class AppComponent {
}
