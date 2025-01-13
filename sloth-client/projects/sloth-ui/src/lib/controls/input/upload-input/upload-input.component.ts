import { Component, computed, forwardRef, input, signal } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgClass } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BaseInputComponent } from '../base-input.component';
import { EventBlockerDirective } from 'sloth-utilities'; 
@Component({
  selector: 'sl-upload-input',
  imports: [EventBlockerDirective, NgClass, MatIcon, MatButtonModule],
  templateUrl: './upload-input.component.html',
  styleUrl: './upload-input.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => UploadInputComponent),
      multi: true
    }
  ]
})
export class UploadInputComponent extends BaseInputComponent implements ControlValueAccessor {
  isDragover = signal<boolean>(false);
  multiple = input<boolean>(true);
  errorMessage = signal<string | null>(null);
  hasFiles = computed(()=> {
    console.log('this.value()', this.value());
    
    return this.value() ? true : false
  });

  storeFile($event: Event) {
    this.isDragover.set(false);
    this.errorMessage.set(null);

    const files = ($event as DragEvent).dataTransfer?.files;
    if (!files) return;

    if (!this.multiple() && files.length > 1) {
      // Show error when multiple files are dragged but `multiple` is `false`
      this.errorMessage.set('Only one file can be uploaded.');
      return;
    }

    if (this.multiple()) {
      // Merge new files with the existing FileList
      const existingFiles = this.value() ? Array.from(this.value() as FileList) : [];
      const newFiles = Array.from(files).filter(
        (file) => !existingFiles.some((existingFile) => existingFile.name === file.name)
      );

      this.value.set(this.createFileList([...existingFiles, ...newFiles]));
    } else {
      // Replace the file when `multiple` is `false`
      this.value.set(files.item(0));
    }
  }

  get fileArray(): File[] {
    if(this.multiple()) 
      return this.value() ? Array.from(this.value() as FileList) : [];
    else 
      return this.value() ? [this.value() as File] : [];
  }

  createFileList(files: File[]): FileList | null {
    const dataTransfer = new DataTransfer();
    files.forEach((file) => dataTransfer.items.add(file));

    if(dataTransfer.files.length === 0)
      return null
    else return dataTransfer.files 
  }
  
  removeFile(fileToRemove: File) {
    if (this.multiple()) {
      let updatedFiles = Array.from(this.value() as FileList).filter(
        (file) => file.name !== fileToRemove.name
      ) as File[] | null;
      
      if(!updatedFiles || updatedFiles?.length === 0)
        updatedFiles = null;

      this.value.set(updatedFiles ? this.createFileList(updatedFiles) : null);
    } else {
      this.value.set(null);
    }
  }
}

