import { Component, inject, output} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbar } from '@angular/material/toolbar';
import { Router } from '@angular/router';


@Component({
  selector: 'main-header',
  imports: [MatToolbar, MatIcon, MatButtonModule, MatMenuModule],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.scss'
})
export class MainHeaderComponent {
  private router = inject(Router);
  onToggle = output();

  onClick() {
    this.onToggle.emit();
  }

  onLogout() {
    this.router.navigate(['auth', 'login'])
  }
}
