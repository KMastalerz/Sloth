import { Component, effect, inject, input, signal } from '@angular/core';
import { NgClass } from '@angular/common';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { IconService } from '../icon-service/icon.service';

@Component({
  selector: 'sl-icon',
  standalone: true,
  imports: [NgClass],
  template: `<div class="sl-control-image" [ngClass]="class()" [innerHTML]="svgContent()"></div>`,
  styleUrl: './icon.component.scss'
})
export class IconComponent {
  private iconService = inject(IconService);
  private sanitizer = inject(DomSanitizer);
  icon = input.required<string>();
  class = input<string | null>(null);

  protected svgContent = signal<SafeHtml  | null>(null);

  constructor(){
    effect(() => { 
      let icon = this.iconService.getIcon(this.icon());
      if(!icon) return;
      this.svgContent.set(this.sanitizer.bypassSecurityTrustHtml(icon!));

    }, { allowSignalWrites: true });
  }
}
