import { AfterViewInit, Component, ElementRef, input, model, output, viewChild } from '@angular/core';
import { MatSlideToggle, MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ControlComponent } from '../../control.component';

@Component({
  selector: 'sl-toggle',
  imports: [MatSlideToggle, MatSlideToggleModule, ControlComponent],
  templateUrl: './toggle.component.html',
  styleUrl: './toggle.component.scss'
})
export class ToggleComponent implements AfterViewInit {
  themeToggle = viewChild(MatSlideToggle, { read: ElementRef });
  checked = model<boolean>(false);
  change = output<boolean>();
  theme = input<'theme-toggle' | null>(null);
  badge = input<number | string | null>(null);

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
    this.checked.set(!this.checked());
    this.change.emit(this.checked());
  }
}
