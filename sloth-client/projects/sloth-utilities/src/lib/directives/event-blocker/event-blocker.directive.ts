import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[sl-event-blocker]'
})
export class EventBlockerDirective {
  constructor() {
    console.log('EventBlockerDirective constructor!');
  }
  @HostListener('drop', ['$event'])
  @HostListener('dragover', ['$event']) 
  public handleEvent($event: Event) {
    $event.preventDefault();
  }
}
