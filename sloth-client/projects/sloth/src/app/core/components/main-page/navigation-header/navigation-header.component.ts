import { Component, inject } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';
import { MatDialog } from '@angular/material/dialog';
import { AddJobDialogComponent } from './navigation-header-dialogs/add-bug-dialog/add-job-dialog.component';
import { ButtonComponent, HeaderLinkComponent, SnackbarService, SnackbarType} from 'sloth-ui';
import { CreateQuickJobParam, JobService } from 'sloth-http';


@Component({
  selector: 'navigation-header',
  imports: [MatToolbar, ButtonComponent, HeaderLinkComponent],
  templateUrl: './navigation-header.component.html',
  styleUrl: './navigation-header.component.scss'
})
export class NavigationHeaderComponent {
  readonly dialog = inject(MatDialog);
  readonly jobService = inject(JobService);
  readonly snackbarService = inject(SnackbarService);

  
  addBug(): void {
    const dialogRef = this.dialog.open(AddJobDialogComponent);

    dialogRef.afterClosed().subscribe((result: CreateQuickJobParam) => {
      if(result)
        this.createQuickJob(result);
      else if(result === null)
        this.snackbarService.openSnackbar('Not enough data!','Close',5000, SnackbarType.ERROR);

    });
  }

  async createQuickJob(job: CreateQuickJobParam): Promise<void> {
    const result = await this.jobService.createQuickJob(job);

    if(result.success) {
      this.snackbarService.openSnackbar('Job created successfully','Close',5000, SnackbarType.SUCCESS);
    }
    else {
      this.snackbarService.openSnackbar('Error: new job not inserted','Close',5000, SnackbarType.ERROR);
    }
  }
}
