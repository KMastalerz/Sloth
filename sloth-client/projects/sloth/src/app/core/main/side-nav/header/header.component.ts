import { Component, input, signal } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { slothIcons } from '@sloth-ui';

@Component({
  selector: 'sl-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private sanitizer: DomSanitizer){
    this.mainIcon.set(this.sanitizer.bypassSecurityTrustHtml(slothIcons.slothIcon));
  }

  collapsed = input.required<boolean>()
  mainIcon = signal<SafeHtml | undefined>(undefined);
}
