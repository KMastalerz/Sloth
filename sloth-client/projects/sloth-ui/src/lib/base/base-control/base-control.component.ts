import { Component, computed, input, model, OnInit } from '@angular/core';
import { WebControl } from '@sloth-http';
import { OpCom } from '../../op-com/op-com';
import { FormArray, FormGroup } from '@angular/forms';

@Component({
  selector: 'sl-base-control',
  standalone: true,
  template:''
})
export class BaseControl implements OnInit  {
  config = input<WebControl | undefined>(undefined);
  
  controlID = model<string>('')
  // TODO: Add default value
  defaultValue = model<unknown>(undefined);
  icon = model<string>('')
  label = model<string>('')  
  placeholder = model<string>('') 
  tooltip = model<string>('')
  tooltipPosition = model<'above' | 'below' | 'left' | 'right'>('below')
  
  mainForm = model<FormGroup>();
  panelForm = model<FormGroup | FormArray>();
  opCom = model<OpCom>();

  hasLabel = computed<boolean>(()=> !(!this.label()));
  hasIcon = computed<boolean>(()=> !(!this.icon()));

  isRequired = model<boolean>(true);
  isHidden = model<boolean>(false);
  isDisabled = model<boolean>(false);

  ngOnInit(): void {
    if(this.config()) {
      if(!this.controlID()) this.controlID.set(this.config()!.controlID);
      if(!this.icon()) this.icon.set(this.config()!.icon ?? '');
      if(!this.label()) this.label.set(this.config()!.label ?? '');
      if(!this.placeholder()) this.placeholder.set(this.config()!.placeholder ?? '');
      if(!this.tooltip()) this.tooltip.set(this.config()!.tooltip ?? '');
      if(!this.tooltipPosition()) this.tooltipPosition.set(this.config()!.tooltipPosition ?? 'below');
    }
  }
}

