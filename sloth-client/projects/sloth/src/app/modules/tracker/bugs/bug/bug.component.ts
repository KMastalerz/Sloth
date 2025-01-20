import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { GetBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentListComponent, FormComponent, FormMode, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, 
    SnackbarService, SnackbarType, TagComponent, TagListComponent,
    TextInputComponent} from 'sloth-ui';
import { FormGroupService } from 'sloth-utilities';

@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent,
    MarkupInputComponent, SideSectionComponent, TagComponent,
    ButtonComponent, MatDivider, ReactiveFormsModule, 
    TagListComponent, CommentListComponent, TextInputComponent],
    templateUrl: './bug.component.html',
    styleUrl: './bug.component.scss'
})
export class BugComponent implements OnInit {
    private readonly jobServices = inject(JobService);
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);
    private readonly titleService = inject(Title)
    private readonly snackbarService = inject(SnackbarService);
    private readonly formGroupService = inject(FormGroupService);


    formMode = signal<FormMode>(FormMode.Read);
    bugID = input.required<number>();
    jobID = signal<number>(0);
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
            tag: new FormControl(''),
            tagColor: new FormControl(null),
            description: new FormControl(null)
        }),
        status: new FormGroup({
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
    comment = model<string>();

    async ngOnInit(): Promise<void> {
        const response = await this.jobServices.getBugAsync(this.bugID());
        if(response.success) {
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
        var results = await this.jobServices.addJobCommentAsync({
            jobID: this.jobID(),
            comment: this.comment()!
        })

        if(results.success) {
            this.patchArray('comments', results.data ?? [], true);            
            this.snackbarService.openSnackbar('Comment added','Close',5000, SnackbarType.SUCCESS);
        }
        else {
            this.snackbarService.openSnackbar(`Error: ${results.error}`,'Close',5000, SnackbarType.ERROR);
        }
    }

    async onSaveForm(): Promise<void> {
        console.log('[BugComponent] onSaveForm:', this.formGroup());
        
    }

    onToggleEditMode() : void {
        this.formMode.set(this.formMode() === FormMode.Read ? FormMode.Edit : FormMode.Read)
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
        }
    }
}
