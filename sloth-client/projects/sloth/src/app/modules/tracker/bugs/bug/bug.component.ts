import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CacheStatusItem, GetBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentListComponent, FormComponent, FormMode, ListSelectComponent, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, 
    SnackbarService, SnackbarType, TagComponent, TagListComponent, ToggleListComponent,
    TextInputComponent} from 'sloth-ui';
import { FormGroupService } from 'sloth-utilities';
import { JobDataCacheService } from '../../../../services/job-data-cache/job-data-cache.service';
@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent,
    MarkupInputComponent, SideSectionComponent, TagComponent,
    ButtonComponent, MatDivider, ReactiveFormsModule,
    TagListComponent, CommentListComponent, TextInputComponent, ListSelectComponent, ToggleListComponent],
    templateUrl: './bug.component.html',
    styleUrl: './bug.component.scss'
})
export class BugComponent implements OnInit {
    private readonly jobDataCacheService = inject(JobDataCacheService)
    private readonly jobServices = inject(JobService);
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    private readonly titleService = inject(Title)
    private readonly snackbarService = inject(SnackbarService);
    private readonly formGroupService = inject(FormGroupService);

    constructor(){
        this.jobDataCacheService.clients
            .pipe(takeUntilDestroyed())
            .subscribe((data) => 
            this.clients.set(data)
        );

        this.jobDataCacheService.priorities
            .pipe(takeUntilDestroyed())
            .subscribe((data) => 
            this.priorities.set(data)
        );

        this.jobDataCacheService.products
            .pipe(takeUntilDestroyed())
            .subscribe((data) => 
            this.products.set(data)
        );

        this.jobDataCacheService.functionalities
            .pipe(takeUntilDestroyed())
            .subscribe((data) => 
            this.functionalities.set(data)
        );

        this.jobDataCacheService.statuses
            .pipe(takeUntilDestroyed())
            .subscribe((data) => 
            this.statuses.set(data.filter(d=> d.type === "Bug" || d.type === "All"))
        );
    }

    clients = signal<CacheClientItem[]>([]);
    priorities = signal<CachePriorityItem[]>([]);
    products = signal<CacheProductItem[]>([]);
    functionalities= signal<CacheFunctionalityItem[]>([]);
    statuses = signal<CacheStatusItem[]>([]);

    formMode = signal<FormMode>(FormMode.Read);
    bugID = input.required<number>();
    jobID = signal<number>(0);
    isRTS = signal<boolean>(false);
    isBlocker = signal<boolean>(false);
    formGroup = signal<FormGroup>(new FormGroup({
        jobID: new FormControl(0),
        header: new FormControl(''),
        description: new FormControl(''),
        type: new FormControl(''),
        createdDate: new FormControl(new Date()),
        updatedDate: new FormControl(null),
        closedDate: new FormControl(null),
        isClosed: new FormControl(false),
        currentOwner: new FormGroup({
            userName: new FormControl(''),
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            fullName: new FormControl(''),
            email: new FormControl('')
        }),
        currentTeam: new FormControl({
            alias: new FormControl(''),
            speciality: new FormControl(''),
            name: new FormControl(''),
            description: new FormControl('')
        }),
        createdBy: new FormGroup({
            userName: new FormControl(''),
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            fullName: new FormControl(''),
            email: new FormControl('')
        }),
        closedBy: new FormGroup({
            userName: new FormControl(''),
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            fullName: new FormControl(''),
            email: new FormControl('')
        }),
        client: new FormGroup({
            name: new FormControl(''),
            alias: new FormControl('')
        }),
        updatedBy: new FormGroup({
            userName: new FormControl(''),
            firstName: new FormControl(''),
            lastName: new FormControl(''),
            fullName: new FormControl(''),
            email: new FormControl('')
        }),
        priority: new FormGroup({
            priorityID: new FormControl(''),
            tag: new FormControl(''),
            tagColor: new FormControl(null),
            description: new FormControl(null)
        }),
        status: new FormGroup({
            statusID: new FormControl(null),
            tag: new FormControl(''),
            tagColor: new FormControl(null),
            description: new FormControl(null)
        }),
        comments: new FormArray([]),
        assignmentHistory: new FormArray([]),
        assignments: new FormArray([]),
        files: new FormArray([]),
        priorityHistory: new FormArray([]),
        statusHistory: new FormArray([]),
        products: new FormArray([]),
        functionalities: new FormArray([])
    }));

    headerID = computed<string>(() => `${this.bugID()}#`)
    isEditMode = computed<boolean>(() => this.formMode() === FormMode.Edit);
    editMode = computed<string>(()=> this.isEditMode() ? 'End edit' : 'Edit');
    currentStatusID = signal<number | null>(null);
    currentProductIDs = signal<number[]>([]);
    currentFunctionalityIDs = signal<number[]>([]);
    currentPriorityID = signal<number | null>(null);

    comment = model<string>();

    async ngOnInit(): Promise<void> {
        const response = await this.jobServices.getBugAsync(this.bugID());
        if(response.success) {
            this.isRTS.set(!!response.data?.client);
            this.isBlocker.set(response.data?.isBlocker ?? false);
            this.currentStatusID.set(response.data?.status?.statusID ?? null);
            this.currentProductIDs.set(response.data?.products.map(p=>p.productID) ?? []);
            this.currentFunctionalityIDs.set(response.data?.functionalities.map(p=>p.functionalityID) ?? []);
            this.currentPriorityID.set(response.data?.priority?.priorityID ?? null)
            this.titleService.setTitle(`${this.bugID()}# ${response.data?.header}`)
            if(response.data) {
                this.jobID.set(response.data.jobID);
                this.patchBug(response.data);
            }

            if(!response.data)
                this.router.navigate(['../'], { relativeTo: this.route });
        }
        else {
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }

    async onAddComment(): Promise<void> {
        var response = await this.jobServices.addJobCommentAsync({
            jobID: this.jobID(),
            comment: this.comment()!
        })

        if(response.success) {
            this.comment.set(undefined);
            this.patchArray('comments', response.data ?? [], true);   
            this.snackbarService.openSnackbar('Comment added','Close',5000, SnackbarType.SUCCESS);
        }
        else {
            this.snackbarService.openSnackbar(`Error: ${response.error}`,'Close',5000, SnackbarType.ERROR);
        }
    }

    async onSaveForm(): Promise<void> {
        console.log('[BugComponent] onSaveForm:', this.formGroup());
        
    }

    onToggleEditMode() : void {
        this.formMode.set(this.formMode() === FormMode.Read ? FormMode.Edit : FormMode.Read)
    }

    onStatusChange(value: number): void {
        const statusGroup = this.formGroup().get('status');
        const status = this.statuses().find(s=> s.statusID === value);

        if(statusGroup) {
            statusGroup.patchValue(status);
            this.currentStatusID.set(value);
        }
    }

    onProductsChange(value: number[]) {
        const productsArray = this.formGroup().get('products') as FormArray;
        const products = this.products().filter(p => value.some(v => v === p.productID)) ?? [];

        if(productsArray) {
            this.patchArray('products', products, true);
            this.currentProductIDs.set(value);
        }
    }

    onFunctionalityChange(value: number[]) {
        const functionalitiesArray = this.formGroup().get('functionalities') as FormArray;
        const functionalities = this.functionalities().filter(f => value.some(v => v === f.functionalityID)) ?? [];

        if(functionalitiesArray) {
            this.patchArray('functionalities', functionalities, true);
            this.currentFunctionalityIDs.set(value);
        } 
    }

    onPriorityChange(value: number) {
        const priorityGroup = this.formGroup().get('priority') 
        const priority = this.priorities().find(p=> p.priorityID === value);

        if(priorityGroup) {
            priorityGroup.patchValue(priority);
            this.currentPriorityID.set(value);
        }
    }

    private patchBug(bug: GetBugItem): void {
        this.formGroup().patchValue(bug);       
        this.patchArray('products', bug.products ?? []);
        this.patchArray('functionalities', bug.functionalities ?? []);
        this.patchArray('comments', bug.comments ?? []);
        this.formGroup.set(this.formGroup());
    }

    private patchArray(key: string, data: unknown[], clearFlag: boolean = false): void {
        const formArray = this.formGroup().get(key);

        if(clearFlag) {
            if(formArray instanceof FormArray)
                formArray.clear();
        }

        if(formArray instanceof FormArray) {
            data.forEach(item=> {
                const newFormGroup = this.formGroupService.createFormFromObject(item);
                formArray.push(newFormGroup);
            })
            formArray.markAsDirty();
        }
    }
}
