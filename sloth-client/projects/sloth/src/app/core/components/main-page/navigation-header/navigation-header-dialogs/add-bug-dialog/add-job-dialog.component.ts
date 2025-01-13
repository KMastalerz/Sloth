import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ListSelectComponent, MarkupInputComponent, SectionComponent, 
  TextInputComponent, ToggleListComponent, CheckboxComponent, DatePickerComponent, 
  TimePickerComponent,  UploadInputComponent} from 'sloth-ui';
import { ListSelectItem, ToggleListItem } from 'sloth-utilities';
import { CreateQuickJobParam } from 'sloth-http';
import { JobDataCacheService } from '../../../../../../services/job-data-cache/job-data-cache.service';


@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogContent, SectionComponent, MatDialogActions,
    MatButtonModule, ReactiveFormsModule, MatDialogTitle,
    ListSelectComponent, MarkupInputComponent, ToggleListComponent,
    TextInputComponent, UploadInputComponent, CheckboxComponent, 
    DatePickerComponent, TimePickerComponent, MatStepperModule],
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
    file: new FormControl<FileList | null>(null),
    raisedDate: new FormControl(new Date(), {
      validators: [Validators.required]
    })
  })


  priotities = signal<ToggleListItem[]>([]);
  products = signal<ListSelectItem[]>([]);
  quickJobTypes = signal<ListSelectItem[]>([]);
  
  onCloseDialog(): void {
    this.dialogRef.close(
      undefined
    );
  }

  onSaveBug(): void {
    console.log('this.jobForm', this.jobForm);
    
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
