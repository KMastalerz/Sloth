import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CacheStatusItem, GetAssignmentBugItem, GetCommentBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentListComponent, FormComponent, FormMode, ListSelectComponent, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, TagComponent, TagListComponent, ToggleListComponent,
    TextInputComponent} from 'sloth-ui';
import { AuthStateService, FormGroupService } from 'sloth-utilities';
import { JobDataCacheService } from '../../../../services/job-data-cache/job-data-cache.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent,
    MarkupInputComponent, SideSectionComponent, TagComponent,
    ButtonComponent, MatDivider, ReactiveFormsModule,
    TagListComponent, CommentListComponent, TextInputComponent, ListSelectComponent, ToggleListComponent],
    templateUrl: './bug.component.html',
    styleUrl: './bug.component.scss',
    providers: [JobDataCacheService]
})
export class BugComponent implements OnInit {
    private readonly authStateService = inject(AuthStateService);
    private readonly jobDataCacheService = inject(JobDataCacheService)
    private readonly jobServices = inject(JobService);
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    private readonly titleService = inject(Title)
    private readonly snackBar = inject(MatSnackBar);
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
            {
                this.products.set(data);
                this.jobDataCacheService.listFunctionalitiesWithProductIDAsync(data.map(p=>p.productID));
            }
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
    bugID = input.required<number>();

    clients = signal<CacheClientItem[]>([]);
    priorities = signal<CachePriorityItem[]>([]);
    products = signal<CacheProductItem[]>([]);
    functionalities= signal<CacheFunctionalityItem[]>([]);
    statuses = signal<CacheStatusItem[]>([]);

    formMode = signal<FormMode>(FormMode.Read);

    canDelete = signal<boolean>(false);
    assignees = signal<GetAssignmentBugItem[]>([]);
    jobID = signal<number>(0);
    isRTS = signal<boolean>(false);
    isBlocker = signal<boolean>(false);
    comments = signal<GetCommentBugItem[]>([]);
    currentClientID = signal<string | null>(null);
    currentStatusID = signal<number | null>(null);
    currentProductIDs = signal<number[]>([]);
    currentFunctionalityIDs = signal<number[]>([]);
    currentPriorityID = signal<number | null>(null);
    formGroup: FormGroup | null = null;

    headerID = computed<string>(() => `${this.bugID()}#`)
    isEditMode = computed<boolean>(() => this.formMode() === FormMode.Edit);
    editMode = computed<string>(()=> this.isEditMode() ? 'End edit' : 'Edit');
    isClaimed = computed<boolean>(()=> this.assignees().some(a=>a.assignedTo === this.authStateService.user?.userName));

    comment = model<string>();
    
    async ngOnInit(): Promise<void> {
        this.canDelete.set(this.authStateService.user?.userRoles.some(r=>r.roleCode === "ADMIN") ?? false);        
        const response = await this.jobServices.getBugAsync(this.bugID());
        if(response.success) {
            if(!response.data)
                this.router.navigate(['../'], { relativeTo: this.route });

            this.jobID.set(response.data!.jobID);
            this.isRTS.set(!!response.data?.client);
            this.isBlocker.set(response.data?.isBlocker ?? false);
            this.currentClientID.set(response.data?.client?.clientID ?? null);
            this.currentStatusID.set(response.data?.status?.statusID ?? null);
            this.currentProductIDs.set(response.data?.products.map(p=>p.productID) ?? []);
            this.currentFunctionalityIDs.set(response.data?.functionalities.map(p=>p.functionalityID) ?? []);
            this.currentPriorityID.set(response.data?.priority?.priorityID ?? null)
            this.comments.set(response.data?.comments ?? []);
            this.assignees.set(response.data?.assignments ?? []);
            this.titleService.setTitle(`${this.bugID()}# ${response.data?.header}`)
            this.formGroup = this.formGroupService.createFormFromObject(response.data) as FormGroup;

            if(this.currentClientID())
                this.jobDataCacheService.listProductsWithClientIDAsync(this.currentClientID());
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
            this.comments.set(response.data);
            this.snackBar.open('Comment added','Close', {duration: 5000});
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onSaveForm(): Promise<void> {
        console.log('[BugComponent] onSaveForm:', this.formGroup);
        
    }

    async onClaimBug(): Promise<void> {
        const response = await this.jobServices.claimBugAsync(this.bugID());
        if(response.success) {
            this.snackBar.open('Bug claimed!','Close', {duration: 5000});
            this.assignees.set(response.data);
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onAbandonBug(): Promise<void> {
        const response = await this.jobServices.abdandonBugAsync(this.bugID());
        if(response.success) {
            this.snackBar.open('Bug abandoned!','Close', {duration: 5000});
            this.assignees.set(response.data);
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onDeleteBug(): Promise<void> {
        const response = await this.jobServices.deleteBugAsync(this.bugID());
        if(response.success) {
            this.snackBar.open('Bug removed!','Close', {duration: 5000});
            this.router.navigate(['../'], { relativeTo: this.route });
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    onToggleEditMode() : void {
        this.formMode.set(this.formMode() === FormMode.Read ? FormMode.Edit : FormMode.Read)
    }

    onClientChange(value: string | null): void {
        const clientGroup = this.formGroup?.get('client');
        const client = this.clients().find(s=> s.clientID === value);

        if(clientGroup) {
            clientGroup.patchValue(client);
            this.currentClientID.set(value);
            this.jobDataCacheService.listProductsWithClientIDAsync(value);
        }
    }

    onStatusChange(value: number): void {
        const statusGroup = this.formGroup?.get('status');
        const status = this.statuses().find(s=> s.statusID === value);

        if(statusGroup) {
            statusGroup.patchValue(status);
            this.currentStatusID.set(value);
        }
    }

    onProductsChange(value: number[]) {
        const productsArray = this.formGroup?.get('products') as FormArray;
        const products = this.products().filter(p => value.some(v => v === p.productID)) ?? [];

        if(productsArray) {
            this.patchArray('products', products, true);
            this.currentProductIDs.set(value);
            this.jobDataCacheService.listFunctionalitiesWithProductIDAsync(value);
        }
    }

    onFunctionalityChange(value: number[]) {
        const functionalitiesArray = this.formGroup?.get('functionalities') as FormArray;
        const functionalities = this.functionalities().filter(f => value.some(v => v === f.functionalityID)) ?? [];

        if(functionalitiesArray) {
            this.patchArray('functionalities', functionalities, true);
            this.currentFunctionalityIDs.set(value);
        } 
    }

    onPriorityChange(value: number) {
        const priorityGroup = this.formGroup?.get('priority') 
        const priority = this.priorities().find(p=> p.priorityID === value);

        if(priorityGroup) {
            priorityGroup.patchValue(priority);
            this.currentPriorityID.set(value);
        }
    }

    private patchArray(key: string, data: unknown[], clearFlag: boolean = false, markAsDirty: boolean = true): void {
        const formArray = this.formGroup?.get(key);

        if(clearFlag) {
            if(formArray instanceof FormArray)
                formArray.clear();
        }

        if(formArray instanceof FormArray) {
            data.forEach(item=> {
                const newFormGroup = this.formGroupService.createFormFromObject(item);
                formArray.push(newFormGroup);
            })

            if(markAsDirty)
                formArray.markAsDirty();
        }
    }
}
