import { Component, computed, ElementRef, forwardRef, input, signal, viewChild } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { v4 as uuidv4 } from 'uuid';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-file-input',
  imports: [MatInputModule, ReactiveFormsModule, FormsModule, MatButtonModule, MatIconModule, MatTooltipModule],
  templateUrl: './file-input.component.html',
  styleUrl: './file-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileInputComponent),
      multi: true
    }
  ]
})
export class FileInputComponent extends BaseFormControlComponent {
  name = input<string>(this.formControlName() ?? uuidv4());
  placeholder = input<string>('');
  label = input<string | null>(null);
  tooltip = input<string | null>(null);
  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');

  hideTooltip = computed(() => !this.tooltip());

  fileInput = viewChild('fileInput', { read: ElementRef });
  fileName = signal<string>('')

  innerOpenFile(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files && input.files.length > 0) {
      const file = input.files[0];
      this.value.set(file);
      this.fileName.set(file.name);
    }
  }

  openFile() {
    this.fileInput()!.nativeElement.click();
  }
}
