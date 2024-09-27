import { Component, computed, input, OnInit, signal } from '@angular/core';
import { WebControl } from '@sloth-http';
import { PageSync } from '../../page/page-sync';

@Component({
  selector: 'sl-base-control',
  standalone: true,
  template:''
})
export class BaseControl implements OnInit {

  config = input<WebControl>();
  pageSync = input<PageSync>();

  metaData = signal<any>(undefined);
  tooltip = signal<string>('');  
  label = signal<string>('');  
  placeholder = signal<string>('');
  route = signal<string>('');  

  ngOnInit(): void {
    if(this.config()) {
      this.tooltip.set(this.config()!.controlTooltip ?? '');
      this.label.set(this.config()!.controlLabel ?? '');
      this.route.set(this.config()!.route ?? '');
      this.placeholder.set(this.config()!.controlPlaceholder ?? '');
  
      if(this.config()!.metaData){
        return this.metaData.set(JSON.parse(this.config()!.metaData!));
      }
    }
  }
}
