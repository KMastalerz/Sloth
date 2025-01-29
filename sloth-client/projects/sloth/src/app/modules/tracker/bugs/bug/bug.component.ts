import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CacheStatusItem, GetAssignmentBugItem, GetBugItem, GetCommentBugItem, GetFunctionalityBugItem, GetProductBugItem, JobService, SaveBugParam } from 'sloth-http';
import { ButtonComponent, CommentListComponent, FormComponent, FormMode, ListSelectComponent, 
    MarkdownInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, TagComponent, TagListComponent, ToggleListComponent,
    TextInputComponent} from 'sloth-ui';
import { ArrayService, AuthStateService, FormGroupService } from 'sloth-utilities';
import { JobDataCacheService } from '../../../../services/job-data-cache/job-data-cache.service';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent,
    MarkdownInputComponent, SideSectionComponent, TagComponent,
    ButtonComponent, MatDivider, ReactiveFormsModule,
    TagListComponent, CommentListComponent, TextInputComponent, ListSelectComponent, ToggleListComponent],
    templateUrl: './bug.component.html',
    styleUrl: './bug.component.scss',
    providers: [JobDataCacheService]
})
export class BugComponent implements OnInit {
    private readonly arrayService = inject(ArrayService);
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
    originalClientID = signal<string | null>(null);
    originalStatusID = signal<number | null>(null);
    originalProductIDs = signal<number[]>([]);
    originalFunctionalityIDs = signal<number[]>([]);
    originalPriorityID = signal<number | null>(null);
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

            this.initialize(response.data!);
        }
        else {
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }

    async initialize(bug: GetBugItem) {
        this.formGroup = null;
        this.jobID.set(bug.jobID);
        this.isRTS.set(!!bug.client);
        this.isBlocker.set(bug.isBlocker ?? false);
        this.currentClientID.set(bug.client?.clientID ?? null);
        this.currentStatusID.set(bug.status?.statusID ?? null);
        this.currentProductIDs.set(bug.products.map(p=>p.productID) ?? []);
        this.currentFunctionalityIDs.set(bug.functionalities.map(p=>p.functionalityID) ?? []);
        this.currentPriorityID.set(bug.priority?.priorityID ?? null)
        this.originalClientID.set(this.currentClientID());
        this.originalStatusID.set(this.currentStatusID());
        this.originalProductIDs.set(this.currentProductIDs());
        this.originalFunctionalityIDs.set(this.currentFunctionalityIDs());
        this.originalPriorityID.set(this.currentPriorityID())
        this.comments.set(bug.comments ?? []);
        this.assignees.set(bug.assignments ?? []);
        this.titleService.setTitle(`${this.bugID()}# ${bug.header}`)
        this.formGroup = this.formGroupService.createFormFromObject(bug) as FormGroup;

        if(this.currentClientID())
            this.jobDataCacheService.listProductsWithClientIDAsync(this.currentClientID());
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
        if(!this.formGroup) {
            this.snackBar.open('Error: Cannot read form', 'Close', {duration: 5000});
            return;
        }

        if(this.formGroup.pristine) {
            this.snackBar.open('Error: Nothing changed', 'Close', {duration: 5000});
            return;
        }

        let param: SaveBugParam = {
            bugID: this.bugID(),
            clientChanged: false,
            newClientID: null,
            priorityChanged: false,
            newPriorityID: null,
            statusChanged: false,
            newStatusID: null,
            newFunctionalityIDs: [],
            removedFunctionalityIDs: [],
            newProductIDs: [],
            removedProductIDs: [],
            newHeader: null,
            newDescription: null
        };
        let header = this.formGroup.get('header');

        if(header && header.dirty) {
            param.newHeader = header.value;
        }

        let description = this.formGroup.get('description');

        if(description && description.dirty) {
            param.newDescription = description.value;
        }
        
        if(this.currentClientID() !== this.originalClientID()) {
            param.newClientID = this.currentClientID();
            param.clientChanged = true;
        }

        if(this.currentPriorityID() !== this.originalPriorityID()) {
            param.newPriorityID = this.currentPriorityID();
            param.priorityChanged = true;
        }

        if(this.currentStatusID() !== this.originalStatusID()) {
            param.newStatusID = this.currentStatusID();
            param.statusChanged = true;
        }

        if(!this.arrayService.areNumberArraysEqual(this.originalProductIDs(), this.currentProductIDs())) {
            param.newProductIDs = this.arrayService.getMissingInSource(this.originalProductIDs(), this.currentProductIDs());
            param.removedProductIDs = this.arrayService.getMissingInSource(this.currentProductIDs(), this.originalProductIDs());
        }

        if(!this.arrayService.areNumberArraysEqual(this.originalFunctionalityIDs(), this.currentFunctionalityIDs())) {
            param.newFunctionalityIDs = this.arrayService.getMissingInSource(this.originalFunctionalityIDs(), this.currentFunctionalityIDs());
            param.removedFunctionalityIDs = this.arrayService.getMissingInSource(this.currentFunctionalityIDs(), this.originalFunctionalityIDs());
        }

        var response = await this.jobServices.SaveBugAsync(param);

        if(response.success) {
            this.snackBar.open('Bug saved!','Close', {duration: 5000});

            if(response.data)
                this.initialize(response.data!);
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onClaimBug(): Promise<void> {
        const response = await this.jobServices.claimJobAsync(this.jobID());
        if(response.success) {
            this.snackBar.open('Bug claimed!','Close', {duration: 5000});
            this.assignees.set([]);
            console.log('response.data', response.data);
            
            this.assignees.set(response.data);
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onAbandonBug(): Promise<void> {
        const response = await this.jobServices.abdandonJobAsync(this.jobID());
        if(response.success) {
            this.snackBar.open('Bug abandoned!','Close', {duration: 5000});
            this.assignees.set([]);
            console.log('response.data', response.data);
            
            this.assignees.set(response.data);
        }
        else {
            this.snackBar.open(`Error: ${response.error}`,'Close', {duration: 5000});
        }
    }

    async onDeleteBug(): Promise<void> {
        const response = await this.jobServices.deleteJobAsync(this.jobID());
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

        this.checkFormGroupState();
    }

    onStatusChange(value: number): void {
        const statusGroup = this.formGroup?.get('status');
        const status = this.statuses().find(s=> s.statusID === value);

        if(statusGroup) {
            statusGroup.patchValue(status);
            this.currentStatusID.set(value);
        }

        this.checkFormGroupState();
    }

    onProductsChange(value: number[]) {
        const productsArray = this.formGroup?.get('products') as FormArray;
        const products = this.products().filter(p => value.some(v => v === p.productID)) ?? [];

        if(productsArray) {
            this.patchArray('products', products, true);
            this.currentProductIDs.set(value);
            this.jobDataCacheService.listFunctionalitiesWithProductIDAsync(value);
        }

        this.checkFormGroupState();
    }

    onFunctionalityChange(value: number[]) {
        const functionalitiesArray = this.formGroup?.get('functionalities') as FormArray;
        const functionalities = this.functionalities().filter(f => value.some(v => v === f.functionalityID)) ?? [];

        if(functionalitiesArray) {
            this.patchArray('functionalities', functionalities, true);
            this.currentFunctionalityIDs.set(value);
        }

        this.checkFormGroupState();
    }

    onPriorityChange(value: number) {
        const priorityGroup = this.formGroup?.get('priority') 
        const priority = this.priorities().find(p=> p.priorityID === value);

        if(priorityGroup) {
            priorityGroup.patchValue(priority);
            this.currentPriorityID.set(value);
        }

        this.checkFormGroupState();
    }

    private patchArray(key: string, data: unknown[], clearFlag: boolean = false): void {
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
        }
    }

    private checkFormGroupState() {
        let hasDirtyValue: boolean = false;
        var client = this.formGroup?.get('client');
        if(this.currentClientID() !== this.originalClientID()) {
            this.markAsDirty(client);
            hasDirtyValue = true;
        }
        else this.markAsPristine(client);

        var status = this.formGroup?.get('status');
        if(this.currentStatusID() !== this.originalStatusID()) {
            this.markAsDirty(status);
            hasDirtyValue = true;
        }
        else this.markAsPristine(status);

        var priority = this.formGroup?.get('priority');
        if(this.currentPriorityID() !== this.originalPriorityID()) {
            this.markAsDirty(priority);
            hasDirtyValue = true;
        }
        else this.markAsPristine(priority);

        var products = this.formGroup?.get('products');
        if(!this.arrayService.areNumberArraysEqual(this.currentProductIDs(), this.originalProductIDs())) {
            this.markAsDirty(products);
            hasDirtyValue = true;
            
            //recursively check its item changed
            if(products)
                (products as FormArray).controls.forEach(c=> {
                    if(!this.originalProductIDs().some(p => p === c.value.productID)) {
                        this.markAsDirty(c);
                    }
                });
        }
        else this.markAsPristine(products);

        var functionalities = this.formGroup?.get('functionalities');
        if(!this.arrayService.areNumberArraysEqual(this.currentFunctionalityIDs(), this.originalFunctionalityIDs())) {
            this.markAsDirty(functionalities);
            hasDirtyValue = true;

            //recursively check its item changed
            if(functionalities)
                (functionalities as FormArray).controls.forEach(c=> {
                    if(!this.originalFunctionalityIDs().some(f => f === c.value.functionalityID)) {
                        this.markAsDirty(c);
                    }
                });
        }
        else this.markAsPristine(functionalities)

        if (hasDirtyValue) this.markAsDirty(this.formGroup);
        else this.markAsPristine(this.formGroup);
    }


    private markAsDirty(formPart: AbstractControl | null | undefined) {
        formPart?.markAsDirty();
        formPart?.markAsTouched();
    }

    private markAsPristine(formPart: AbstractControl | null | undefined) {
        formPart?.markAsPristine();
        formPart?.markAsUntouched();
    }
}
