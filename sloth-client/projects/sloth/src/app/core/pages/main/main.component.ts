import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BasePage, DynamicRouterFormComponent} from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [RouterOutlet, DynamicRouterFormComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {}

