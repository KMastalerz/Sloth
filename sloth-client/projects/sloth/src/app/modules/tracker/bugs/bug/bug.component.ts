import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { MatDivider } from '@angular/material/divider';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { GetBugItem, JobService } from 'sloth-http';
import { ButtonComponent, CommentComponent, FormComponent, FormMode, 
    MarkupInputComponent, SectionComponent, SideFormComponent, SideSectionComponent, 
    SnackbarService, SnackbarType, TagComponent } from 'sloth-ui';
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

    descriptionMode = signal(FormMode.Read);
    bugID = input.required<number>();
    bug = model<GetBugItem | undefined>(undefined);
    
    mainHeader = computed<string>(()=>  {
        const mainHead = `${this.bugID()}#\t  ${this.bug()?.header}`;
        this.titleService.setTitle(mainHead);
        return mainHead;
    });
    comment = model<string>();

    async ngOnInit(): Promise<void> {
        var response = await this.jobServices.getBugAsync(this.bugID());

        if(response.success) {
            this.bug.set(response.data ?? undefined);
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
