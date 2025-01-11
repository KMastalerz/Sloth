import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[sl-event-blocker]'
})
export class EventBlockerDirective {

  @HostListener('drop', ['$event']) 
  @HostListener('dragover', ['$event']) 
  public handleEvent(event: Event) {
    event.preventDefault();
  }
}
