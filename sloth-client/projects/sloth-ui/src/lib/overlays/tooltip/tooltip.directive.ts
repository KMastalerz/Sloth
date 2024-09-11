import { Directive, ElementRef, HostListener, inject, input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[slTooltip]',
  standalone: true,
  host: {
    'mouseleave' : 'hideTooltip()',
    'mouseenter' : 'showTooltip()'
  }
})
export class TooltipDirective {
  private el = inject(ElementRef);
  private renderer = inject(Renderer2)

  tooltipText = input.required<string>();
  position = input<'top' | 'bottom' | 'left' | 'right'>('top');

  tooltipElement: HTMLElement | null = null;


  @HostListener('mouseenter') onMouseEnter() {
    if (!this.tooltipElement) {
      this.showTooltip();
    }
  }

  @HostListener('mouseleave') onMouseLeave() {
    if (this.tooltipElement) {
      this.hideTooltip();
    }
  }

  private showTooltip(): void {
    if (this.tooltipElement) return;
    this.tooltipElement = this.renderer.createElement('span');
    this.tooltipElement!.innerText = this.tooltipText();

    this.renderer.appendChild(document.body, this.tooltipElement);
    this.renderer.addClass(this.tooltipElement, 'tooltip');
    this.renderer.addClass(this.tooltipElement, `tooltip-${this.position}`);

    const hostPos = this.el.nativeElement.getBoundingClientRect();
    const tooltipPos = this.tooltipElement!.getBoundingClientRect();

    let top, left;

    switch (this.position()) {
      case 'top':
        top = hostPos.top - tooltipPos.height;
        left = hostPos.left + (hostPos.width - tooltipPos.width) / 2;
        break;
      case 'bottom':
        top = hostPos.bottom;
        left = hostPos.left + (hostPos.width - tooltipPos.width) / 2;
        break;
      case 'left':
        top = hostPos.top + (hostPos.height - tooltipPos.height) / 2;
        left = hostPos.left - tooltipPos.width;
        break;
      case 'right':
        top = hostPos.top + (hostPos.height - tooltipPos.height) / 2;
        left = hostPos.right;
        break;
    }

    this.renderer.setStyle(this.tooltipElement, 'top', `${top}px`);
    this.renderer.setStyle(this.tooltipElement, 'left', `${left}px`);
  }

  private hideTooltip(): void {
    if (!this.tooltipElement) return;
    this.renderer.removeChild(document.body, this.tooltipElement);
    this.tooltipElement = null;
  }
}
