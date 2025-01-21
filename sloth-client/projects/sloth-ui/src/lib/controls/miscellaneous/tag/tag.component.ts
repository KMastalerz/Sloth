import { NgStyle } from '@angular/common';
import { Component, computed, forwardRef, inject, input, model, OnDestroy, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { HexService } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { Subscription } from 'rxjs';

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
export class TagComponent extends BaseFormControlComponent implements OnInit, OnDestroy {
  private readonly herService = inject(HexService);
  // Possible inputs
  backgroundControlName = input<string | null | undefined>(undefined);
  tooltipControlName = input<string | null | undefined>(undefined);

  tooltip = model<string | null | undefined>(undefined);
  backgroundColor = model<string | null | undefined>(null);

  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  fallbackValue = input<string | null | undefined>(undefined);

  // Computed values for UI
  hideTooltip = computed<boolean>(()=> this.tooltip() === '');
  color = computed(()=> this.herService.getAccessibleFontColor(this.backgroundColor()) ?? null);
  displayValue = computed(()=> this.value() ? this.value() : this.fallbackValue());

  formGroupSubscription: Subscription | undefined = undefined;

  override ngOnInit(): void {
    super.ngOnInit();

    if(this.formGroup()) {
      // Get the current values from the form group
      const currentValue = this.formGroup()!.value;
      
      // Initialize backgroundColor and tooltip using current form values
      if (this.backgroundControlName()) {
        this.backgroundColor.set(currentValue[this.backgroundControlName()!]);
      }
      if (this.tooltipControlName()) {
        this.tooltip.set(currentValue[this.tooltipControlName()!]);
      }
      
      this.formGroupSubscription = this.formGroup()?.valueChanges.subscribe(value => {        
        if(this.backgroundControlName()) {
          this.backgroundColor.set(value[this.backgroundControlName()!]);
        }
        if(this.tooltipControlName())
          this.tooltip.set(value[this.tooltipControlName()!]);
      });
    }
  }

  override ngOnDestroy(): void {
    super.ngOnDestroy();

    if(this.formGroupSubscription) {
      this.formGroupSubscription.unsubscribe();
      this.formGroupSubscription = undefined;
    }
  }
}
