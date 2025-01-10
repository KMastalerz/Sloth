import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FileInputComponent, ListSelectComponent, MarkupInputComponent, SectionComponent, TextInputComponent, ToggleListComponent } from 'sloth-ui';
import { ListSelectItem, ToggleListItem } from 'sloth-utilities';
import { JobDataCacheService } from '../../../../../../services/job-data-cache/job-data-cache.service';

@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogContent, SectionComponent, MatDialogActions,
    MatButtonModule, ReactiveFormsModule, MatDialogTitle,
    ListSelectComponent, MarkupInputComponent, ToggleListComponent,
    TextInputComponent, FileInputComponent],
  templateUrl: './add-bug-dialog.component.html',
  styleUrl: './add-bug-dialog.component.scss'
})
export class AddBugDialogComponent   {
  readonly dialogRef = inject(MatDialogRef<AddBugDialogComponent>);
  readonly jobDataCacheService = inject(JobDataCacheService)

  constructor(){
    this.jobDataCacheService.jobPriorities
    .pipe(takeUntilDestroyed())
    .subscribe((data) => 
      this.priotities.set(data)
    );

    this.jobDataCacheService.products
      .pipe(takeUntilDestroyed())
      .subscribe((data) => 
        this.products.set(data)
    );

    this.jobDataCacheService.quickJobTypes
      .pipe(takeUntilDestroyed())
      .subscribe((data) => 
        this.quickJobTypes.set(data)
    );
  }

  onStatusChange(status: any) {
    console.log('status', status);
    
  }

  protected jobForm = new FormGroup({
    type: new FormControl('', {
      validators: [Validators.required]
    }),
    header: new FormControl('', {
      validators: [Validators.required]
    }),
    description: new FormControl('', {
      validators: [Validators.required]
    }),
    priority: new FormControl('', {
      validators: [Validators.required]
    }),
    products: new FormControl('', {
      validators: [Validators.required]
    }),
    file: new FormControl<File | null>(null), // Allow File objects
  })


  priotities = signal<ToggleListItem[]>([]);
  products = signal<ListSelectItem[]>([]);
  quickJobTypes = signal<ListSelectItem[]>([]);
  
  onCloseDialog(): void {
    this.dialogRef.close(
      null
    );
  }

  onSaveBug(): void {
    console.log('jobForm', this.jobForm);
    
    if(this.jobForm.valid)
      this.dialogRef.close(
        this.jobForm.value
      );
    else 
      this.dialogRef.close(
        null
      )
  }

  fileToUpload: File | null = null;

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }
}
