import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FileInputComponent, ListSelectComponent, MarkupInputComponent, SectionComponent, 
  TextInputComponent, ToggleListComponent, CheckboxComponent, DatePickerComponent } from 'sloth-ui';
import { ListSelectItem, ToggleListItem } from 'sloth-utilities';
import { JobDataCacheService } from '../../../../../../services/job-data-cache/job-data-cache.service';
import { CreateQuickJobParam } from 'sloth-http';


@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogContent, SectionComponent, MatDialogActions,
    MatButtonModule, ReactiveFormsModule, MatDialogTitle,
    ListSelectComponent, MarkupInputComponent, ToggleListComponent,
    TextInputComponent, FileInputComponent, CheckboxComponent, DatePickerComponent],
  templateUrl: './add-job-dialog.component.html',
  styleUrl: './add-job-dialog.component.scss'
})
export class AddJobDialogComponent   {
  readonly dialogRef = inject(MatDialogRef<AddJobDialogComponent>);
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
    priorityID: new FormControl(1, {
      validators: [Validators.required]
    }),
    products: new FormControl([] as number[], {
      validators: [Validators.required]
    }),
    isClient: new FormControl(false),
    file: new FormControl<File | null>(null),
    raisedDate: new FormControl(new Date(), {
      validators: [Validators.required]
    }),
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
    if(this.jobForm.valid)
      this.dialogRef.close(
        this.jobForm.value as CreateQuickJobParam
      );
    else 
      this.dialogRef.close(
        null
      )
  }
}
