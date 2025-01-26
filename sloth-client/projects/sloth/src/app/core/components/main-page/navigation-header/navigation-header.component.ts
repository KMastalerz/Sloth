import { Component, inject } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';
import { MatDialog } from '@angular/material/dialog';
import { AddJobDialogComponent } from './navigation-header-dialogs/add-bug-dialog/add-job-dialog.component';
import { ButtonComponent, HeaderLinkComponent} from 'sloth-ui';
import { CreateJobParam, JobService } from 'sloth-http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDivider } from '@angular/material/divider';


@Component({
  selector: 'navigation-header',
  imports: [MatToolbar, ButtonComponent, HeaderLinkComponent, MatDivider],
  templateUrl: './navigation-header.component.html',
  styleUrl: './navigation-header.component.scss'
})
export class NavigationHeaderComponent {
  private readonly dialog = inject(MatDialog);
  private readonly jobService = inject(JobService);
  private readonly snackBar = inject(MatSnackBar);

  
  addBug(): void {
    const dialogRef = this.dialog.open(AddJobDialogComponent);

    dialogRef.afterClosed().subscribe((result: CreateJobParam) => {
      if(result)
        this.createQuickJob(result);
      else if(result === null)
        this.snackBar.open('Not enough data!','Close',{duration: 5000});

    });
  }

  async createQuickJob(job: CreateJobParam): Promise<void> {
    const result = await this.jobService.createJob(job);

    if(result.success) {
      this.snackBar.open('Job created successfully','Close',{duration: 5000});
    }
    else {
      this.snackBar.open('Error: new job not inserted','Close',{duration: 5000});
    }
  }
}
