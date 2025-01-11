import { Component, ElementRef, forwardRef, signal, viewChild } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { ControlComponent } from '../../control.component';
import { FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BaseInputComponent } from '../base-input.component';

@Component({
  selector: 'sl-file-input',
  imports: [MatInputModule, ControlComponent, ReactiveFormsModule, FormsModule, MatButtonModule, MatIconModule],
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
export class FileInputComponent extends BaseInputComponent {
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
