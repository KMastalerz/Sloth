import { P } from '@angular/cdk/keycodes';
import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { FormArray, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { GetBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentListComponent, FormComponent, FormMode, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, 
    SnackbarService, SnackbarType, TagComponent, TagListComponent} from 'sloth-ui';
import { FormGroupService } from 'sloth-utilities';

@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent,
    MarkupInputComponent, SideSectionComponent, TagComponent,
    ButtonComponent, MatDivider, ReactiveFormsModule, 
    TagListComponent, CommentListComponent],
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


    descriptionMode = signal(FormMode.Read);
    bugID = input.required<number>();
    // TODO: remove, try to use form only.
    bug = model<GetBugItem | undefined>(undefined);
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

    mainHeader = computed<string>(()=>  {
        const mainHead = `${this.bugID()}#\t  ${this.bug()?.header}`;
        this.titleService.setTitle(mainHead);
        return mainHead;
    });
    comment = model<string>();

    async ngOnInit(): Promise<void> {
        const response = await this.jobServices.getBugAsync(this.bugID());
        if(response.success) {
            this.bug.set(response.data ?? undefined); // TODO: remove, try to use form only.    
            console.log('Bug', this.bug());
                    
            if(response.data)
                this.patchBug(response.data);
            if(!response.data)
                this.router.navigate(['../'], { relativeTo: this.route });
        }
        else {
            this.router.navigate(['../'], { relativeTo: this.route });
        }
    }

    editDescriptionChange(): void {
        this.descriptionMode() === FormMode.Add ? this.descriptionMode.set(FormMode.Read) : this.descriptionMode.set(FormMode.Add);
    }

    async onAddComment(): Promise<void> {
        var results = await this.jobServices.addJobCommentAsync({
            jobID: this.bug()?.jobID!,
            comment: this.comment()!
        })

        const bug = this.bug();
        if(results.success && bug) {
            bug.comments = results.data
            this.bug.set(bug);
            this.comment.set('');
            this.snackbarService.openSnackbar('Comment added','Close',5000, SnackbarType.SUCCESS);
        }
        else {
            this.snackbarService.openSnackbar(`Error: ${results.error}`,'Close',5000, SnackbarType.ERROR);
        }
    }

    async onSaveForm(): Promise<void> {
        console.log('[BugComponent] onSaveForm:', this.formGroup());
        
    }

    private patchBug(bug: GetBugItem): void {
        this.formGroup().patchValue(bug);       
        this.patchArray('products', bug.products ?? []);
        this.patchArray('functionalities', bug.functionalities ?? []);
        this.patchArray('comments', bug.comments ?? []);
    }

    private patchArray(key: string, data: unknown[]): void {
        const formArray = this.formGroup().get(key);

        if(formArray instanceof FormArray) {
            data.forEach(item=> {
                const newFormGroup = this.formGroupService.createFormFromObject(item);
                formArray.push(newFormGroup);
            })
        }
    }
}
