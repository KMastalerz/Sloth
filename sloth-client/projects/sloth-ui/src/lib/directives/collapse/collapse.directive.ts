import { Directive, ElementRef, inject, input, OnChanges, Renderer2, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[expanded], [collapsed]', 
  standalone: true
})
export class CollapseDirective implements OnChanges {
  private el = inject(ElementRef);
  private renderer = inject(Renderer2);

  expanded = input<boolean | null>(null);
  collapsed = input<boolean | null>(null);

  ngOnChanges(changes: SimpleChanges): void {
    // Toggle the 'expanded' class based on the input
    if (changes['expanded'] && this.expanded() !== null) {
      if (this.expanded()) {
        this.renderer.addClass(this.el.nativeElement, 'expanded');
      } else {
        this.renderer.removeClass(this.el.nativeElement, 'expanded');
      }
    }

    // Toggle the 'collapsed' class based on the input
    if (changes['collapsed'] && this.collapsed() !== null) {
      if (this.collapsed()) {
        this.renderer.addClass(this.el.nativeElement, 'collapsed');
      } else {
        this.renderer.removeClass(this.el.nativeElement, 'collapsed');
      }
    }
  }
}
