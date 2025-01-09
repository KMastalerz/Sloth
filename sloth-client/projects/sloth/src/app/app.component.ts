import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { environment } from './environments/environment';
import { HttpConfigService } from 'sloth-http';

@Component({
    selector: 'app-root',
    imports: [RouterOutlet],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {
    configService = inject(HttpConfigService);
    constructor(){
        this.configService.apiUrl = environment.apiUrl;
    }
}
