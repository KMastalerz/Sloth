import { AfterViewInit, Component, ElementRef, forwardRef, input, output, viewChild } from '@angular/core';
import { MatSlideToggle, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-toggle',
  imports: [MatSlideToggle, MatSlideToggleModule, FormsModule],
  templateUrl: './toggle.component.html',
  styleUrl: './toggle.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ToggleComponent),
      multi: true
    }
  ],
})
export class ToggleComponent extends BaseFormControlComponent implements AfterViewInit {
  themeToggle = viewChild(MatSlideToggle, { read: ElementRef });
  change = output();
  theme = input<'theme-toggle' | null>(null);  
  
  trueIcon = input<string | null>(null);
  falseIcon = input<string | null>(null);

  ngAfterViewInit(): void{
    if(this.themeToggle()) {
      if(this.trueIcon())
        this.themeToggle()!.nativeElement.querySelector('.mdc-switch__icon--on').firstChild.setAttribute('d', this.trueIcon());
      if(this.falseIcon())
        this.themeToggle()!.nativeElement.querySelector('.mdc-switch__icon--off').firstChild.setAttribute('d', this.falseIcon());
    }
  }

  onToggle(): void {
    this.change.emit();
  }
}
