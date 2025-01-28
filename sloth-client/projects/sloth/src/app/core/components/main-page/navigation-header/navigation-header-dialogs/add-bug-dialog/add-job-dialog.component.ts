import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ListSelectComponent, MarkupInputComponent, TextInputComponent, 
  ToggleListComponent, UploadInputComponent,
  TimePickerComponent,
  DatePickerComponent} from 'sloth-ui';
import { CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CreateJobParam } from 'sloth-http';
import { JobDataCacheService } from '../../../../../../services/job-data-cache/job-data-cache.service';

@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogContent, MatDialogActions, MatButtonModule,
    ReactiveFormsModule, MatDialogTitle, ListSelectComponent,
    MarkupInputComponent, ToggleListComponent, TextInputComponent,
    UploadInputComponent, MatStepperModule, DatePickerComponent,
    TimePickerComponent],
  templateUrl: './add-job-dialog.component.html',
  styleUrl: './add-job-dialog.component.scss',
  providers: [JobDataCacheService]
})
export class AddJobDialogComponent {
  private readonly dialogRef = inject(MatDialogRef<AddJobDialogComponent>);
  private readonly jobDataCacheService = inject(JobDataCacheService)

  constructor(){
    this.jobDataCacheService.priorities
      .pipe(takeUntilDestroyed())
      .subscribe((data) => {
        this.priorities.set(data)
      }
    );

    this.jobDataCacheService.products
      .pipe(takeUntilDestroyed())
      .subscribe((data) => 
        this.products.set(data)
    );

    this.jobDataCacheService.jobTypes
      .pipe(takeUntilDestroyed())
      .subscribe((data) => 
        this.jobTypes.set(data)
    );

    this.jobDataCacheService.clients
      .pipe(takeUntilDestroyed())
      .subscribe((data) => {
        this.clients.set(data);
      }
    );

    this.jobDataCacheService.functionalities
      .pipe(takeUntilDestroyed())
      .subscribe((data) => 
        this.functionalities.set(data)
    );

    this.jobForm.controls.clientID.valueChanges
      .pipe(takeUntilDestroyed())
      .subscribe((value) => {
        this.jobDataCacheService.listProductsWithClientIDAsync(value);
        this.jobDataCacheService.listFunctionalitiesWithProductIDAsync(this.products().map(p=>p.productID))
      }
       
    );

    this.jobForm.controls.products.valueChanges
      .pipe(takeUntilDestroyed())
      .subscribe((value) => 
        this.jobDataCacheService.listFunctionalitiesWithProductIDAsync(value)
    );
  }

  protected jobForm = new FormGroup({
    type: new FormControl('', {
      validators: [Validators.required]
    }),
    header: new FormControl('', {
      validators: [Validators.required]
    }),
    clientID: new FormControl(null as string | null),
    description: new FormControl('', {
      validators: [Validators.required]
    }),
    priorityID: new FormControl(1, {
      validators: [Validators.required]
    }),
    products: new FormControl([] as number[], {
      validators: [Validators.required]
    }),
    functionalities: new FormControl([] as number[] | null),
    files: new FormControl<FileList | null>(null),
    raisedDate: new FormControl(new Date(), {
      validators: [Validators.required]
    })
  })

  jobTypes = signal<string[]>([]);
  clients = signal<CacheClientItem[]>([]);
  priorities = signal<CachePriorityItem[]>([]);
  products = signal<CacheProductItem[]>([]);
  functionalities= signal<CacheFunctionalityItem[]>([]);
  
  onCloseDialog(): void {
    this.dialogRef.close(
      undefined
    );
  }

  onSaveBug(): void {
    if(this.jobForm.valid)       
      this.dialogRef.close(
        this.jobForm.value as CreateJobParam
      );
    else 
      this.dialogRef.close(
        null
      )
  }
}
