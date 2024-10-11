import { Component } from '@angular/core';
import { BrandingSectionComponent } from '../../sections/branding-section/branding-section.component';
import { BasePanel } from '../../base/base-panel/base-panel.component';

@Component({
  selector: 'sl-header',
  standalone: true,
  imports: [BrandingSectionComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent extends BasePanel {}
