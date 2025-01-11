import { Component, inject } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddJobDialogComponent } from './navigation-header-dialogs/add-bug-dialog/add-job-dialog.component';
import { ButtonComponent, HeaderLinkComponent } from 'sloth-ui';
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
  readonly snackBar = inject(MatSnackBar);
  
  addBug(): void {
    const dialogRef = this.dialog.open(AddJobDialogComponent);

    dialogRef.afterClosed().subscribe((result: CreateQuickJobParam) => {
      if(result)
        this.createQuickJob(result);
      else 
        this.snackBar.open('Not enough data!', 'Close', {
          duration: 5000,
        });
    });
  }

  async createQuickJob(job: CreateQuickJobParam): Promise<void> {
    const result = await this.jobService.createQuickJob(job);

    if(result.success) {
      this.snackBar.open('Job created successfully!', 'Close', {
        duration: 3000,
      });
    }
    else {
      this.snackBar.open(`Error: ${result.error}`, 'Close', {
        duration: 5000,
      });
    }
  }
}
