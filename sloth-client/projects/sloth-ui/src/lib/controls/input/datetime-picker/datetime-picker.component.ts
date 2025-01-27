import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { Component, computed, ElementRef, forwardRef, inject, input, model, signal, TemplateRef, viewChild, ViewContainerRef } from '@angular/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { v4 as uuidv4 } from 'uuid';
import { DateFormat, DateService } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatCardModule } from '@angular/material/card';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';

@Component({
  selector: 'sl-datetime-picker',
  imports: [ReactiveFormsModule, FormsModule, MatTooltipModule, MatDatepickerModule, MatCardModule],
  templateUrl: './datetime-picker.component.html',
  styleUrl: './datetime-picker.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateTimePickerComponent),
      multi: true
    },
    provideNativeDateAdapter(),
  ],
})
export class DateTimePickerComponent extends BaseFormControlComponent {
  private readonly dateService = inject(DateService);
  private overlayRef: OverlayRef | null = null;
  private overlay = inject(Overlay) 
  private elementRef = inject(ElementRef)
  private viewContainerRef = inject(ViewContainerRef)
  calendarTemplate = viewChild('calendar', { read: TemplateRef });
  
  name = input<string>(this.formControlName() ?? uuidv4());
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  type = input<'dateOnly' | 'dateTimeStandard' | 'dateTimeFull'>('dateTimeStandard');
  showCalendar = signal<boolean>(false);
  hideTooltip = computed(() => !this.tooltip());
  private innerType = computed<DateFormat>(()=> {
    switch(this.type()) {
      case 'dateOnly': 
        return DateFormat.dateOnly;
      case 'dateTimeFull':
        return DateFormat.dateTimeFull;
      case 'dateTimeStandard': 
        return DateFormat.dateTimeStandard;
    }
  })

  protected placeholder = computed<string>(() => this.dateService.getFormat(this.innerType()));
  protected maxLength = computed<number>(() => this.dateService.maxLength(this.innerType()));
  protected innerValue = model<string>('');
  protected dateValue = model<Date>(new Date());
  protected timeValue = model<string>('');

  override ngOnInit(): void {
    super.ngOnInit();

    this.innerValue.set(this.dateService.toString(this.value(), this.innerType()));
    this.dateValue.set(this.value());
  }

  onValueChange(model: string): void {
    if(model.length === this.maxLength()) {
      let value = this.dateService.toDate(model, this.innerType());
      this.value.set(value);
    }
  }

  onCalendarChange(value: Date): void {
    this.innerValue.set(this.dateService.toString(this.value(), this.innerType()));
  }

  onToggleCalendar() {
    if (this.overlayRef && this.overlayRef.hasAttached()) {
      console.log('close');
      
      this.closeCalendar();
    } else {
      console.log('open');
      
      this.openCalendar();
    }
  }

  openCalendar(): void {
    const positionStrategy = this.overlay.position()
      .flexibleConnectedTo(this.elementRef)
      .withPositions([
        {
          originX: 'start',
          originY: 'bottom',
          overlayX: 'start',
          overlayY: 'top',
          offsetY: 8, // 0.5rem if 1rem = 16px
        }
      ]);

    this.overlayRef = this.overlay.create({
      positionStrategy,
      scrollStrategy: this.overlay.scrollStrategies.close(),
      hasBackdrop: true,
      backdropClass: 'transparent-backdrop',
    });

    const portal = new TemplatePortal(this.calendarTemplate()!, this.viewContainerRef);
    this.overlayRef.attach(portal);

    this.overlayRef.backdropClick().subscribe(() => this.closeCalendar());
  }

  closeCalendar(): void {
    if (this.overlayRef) {
      this.overlayRef.detach();
      this.overlayRef.dispose();
      this.overlayRef = null;
    }
  }
}
