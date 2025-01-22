import { Component, computed, ElementRef, forwardRef, input, signal, viewChild } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgClass } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { EventBlockerDirective } from 'sloth-utilities'; 
import { v4 as uuidv4 } from 'uuid';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
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
export class UploadInputComponent extends BaseFormControlComponent   {
  multiple = input<boolean>(true);
  name = input<string>(this.formControlName() ?? uuidv4());

  fileInput = viewChild('fileInput', { read: ElementRef });
  isDragover = signal<boolean>(false);
  hasFiles = computed(()=> {
    return this.value() ? true : false
  });

  storeFile($event: Event) {
    this.isDragover.set(false);

    const files = ($event as DragEvent).dataTransfer?.files;
    if (!files) return;

    if (!this.multiple() && files.length > 1) {
      return;
    }

    if (this.multiple()) {
      const existingFiles = this.value() ? Array.from(this.value() as FileList) : [];
      const newFiles = Array.from(files).filter(
        (file) => !existingFiles.some((existingFile) => existingFile.name === file.name)
      );

      this.value.set(this.createFileList([...existingFiles, ...newFiles]));
    } else {
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

  openFile() {
    this.fileInput()!.nativeElement.click();
  }

  innerOpenFile(event: Event): void {
    const input = event.target as HTMLInputElement;

    if(this.multiple()) {
      if(input.files) {
        const existingFiles = this.value() ? Array.from(this.value() as FileList) : [];
        const newFiles = Array.from(input.files).filter(
          (file) => !existingFiles.some((existingFile) => existingFile.name === file.name)
        );
  
        this.value.set(this.createFileList([...existingFiles, ...newFiles]));
      }
    }
    else {
      this.value.set(input.files?.item(0));
    }
  }
}

