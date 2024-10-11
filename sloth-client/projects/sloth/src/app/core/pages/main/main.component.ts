import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BasePage, DynamicGridComponent} from '@sloth-ui';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [RouterOutlet, DynamicGridComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {}

