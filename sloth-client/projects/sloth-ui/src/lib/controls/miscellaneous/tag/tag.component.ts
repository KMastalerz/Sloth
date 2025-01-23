import { NgStyle } from '@angular/common';
import { Component, computed, forwardRef, inject, input, model, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { HexService } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-tag',
  imports: [NgStyle, MatTooltipModule],
  templateUrl: './tag.component.html',
  styleUrl: './tag.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TagComponent),
      multi: true
    }
  ]
})
export class TagComponent extends BaseFormControlComponent implements OnInit{
  private readonly herService = inject(HexService);
  // Possible inputs
  tag =  model<any | null | undefined>(null);
  tooltip = model<string | null | undefined>(undefined);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  backgroundColor = model<string | null | undefined>(null);
  fallbackValue = input<string | null | undefined>(undefined);

  tagKey =  input<string | null>(null);
  tooltipKey =  input<string | null>(null);
  backgroundKey =  input<string| null>(null);

  // Computed values for UI
  hideTooltip = computed<boolean>(()=> this.tooltip() === '');
  color = computed(()=> this.herService.getAccessibleFontColor(this.backgroundColor()) ?? null);
  displayValue = computed(()=> this.value() ? this.value() : this.fallbackValue());

  override ngOnInit(): void {
    super.ngOnInit();
    
    if(this.formGroup() && !this.tag()) {
      const value = this.formGroup()?.value;
      
      if(this.tagKey()) {
        this.value.set(value[this.tagKey()!])
      }
      if(this.tooltipKey()) {
        this.tooltip.set(value[this.tooltipKey()!])
      }
      if(this.backgroundKey()) {
        this.backgroundColor.set(value[this.backgroundKey()!])
      }
    }
    else if(this.tag()) {
      if(this.tagKey()) {
        this.value.set(this.tag()[this.tagKey()!])
      }
      if(this.tooltipKey()) {
        this.tooltip.set(this.tag()[this.tooltipKey()!])
      }
      if(this.backgroundKey()) {
        this.backgroundColor.set(this.tag()[this.backgroundKey()!])
      }
    }
  }
}
