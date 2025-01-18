import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { GetBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentComponent, FormComponent, FormMode, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, 
    SnackbarService, SnackbarType, TagComponent } from 'sloth-ui';
import { FormGroupService } from 'sloth-utilities';
@Component({
    selector: 'app-bug',
    imports: [FormComponent, SideFormComponent, SectionComponent, 
        MarkupInputComponent, SideSectionComponent, TagComponent, 
        ButtonComponent, MatDivider, CommentComponent],
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
    bug = model<GetBugItem | undefined>(undefined);
    // formGroup = signal<FormGroup | undefined>(undefined);
    formGroup: FormGroup | undefined = undefined;
    mainHeader = computed<string>(()=>  {
        const mainHead = `${this.bugID()}#\t  ${this.bug()?.header}`;
        this.titleService.setTitle(mainHead);
        return mainHead;
    });
    comment = model<string>();

    async ngOnInit(): Promise<void> {
        const response = await this.jobServices.getBugAsync(this.bugID());
        if(response.success) {
            this.bug.set(response.data ?? undefined);
            // this.formGroup.set(this.formGroupService.createFormGroup(this.bug()));
            this.formGroup = this.formGroupService.createFormGroup(this.bug());
            console.log('[BugComponent] ngOnInit:', this.formGroup);
            if(!this.bug())
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
}
