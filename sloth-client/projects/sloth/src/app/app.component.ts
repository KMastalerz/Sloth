import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { HttpConfigService } from '@sloth-http';
import { environment } from '../environments/environment';
import { DynamicRegistrationService } from '@sloth-ui';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'sloth';
  configService = inject(HttpConfigService)
  regService = inject(DynamicRegistrationService);
  constructor() {
    this.configService.apiUrl = environment.apiUrl;
    this.regService.registerElements();
  }
}
