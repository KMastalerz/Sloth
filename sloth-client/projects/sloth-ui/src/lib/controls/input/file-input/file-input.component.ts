import { Component, ElementRef, signal, viewChild } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { ControlComponent } from '../../control.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { BaseFormControlComponent } from '../../base-form-control.component';

@Component({
  selector: 'sl-file-input',
  imports: [MatInputModule, ControlComponent, ReactiveFormsModule, FormsModule, MatButtonModule, MatIconModule],
  templateUrl: './file-input.component.html',
  styleUrl: './file-input.component.scss'
})
export class FileInputComponent extends BaseFormControlComponent {
  fileInput = viewChild('fileInput', { read: ElementRef });
  fileName = signal<string>('')

  innerOpenFile(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files && input.files.length > 0) {
      const file = input.files[0];
      this.writeValue(file); 
      this.fileName.set(file.name);
    }
  }

  openFile() {
    this.fileInput()!.nativeElement.click();
  }
}
