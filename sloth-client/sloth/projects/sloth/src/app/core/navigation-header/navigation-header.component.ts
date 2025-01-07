import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbar } from '@angular/material/toolbar';
import { MatDialog } from '@angular/material/dialog';
import { AddBugDialogComponent } from './navigation-header-dialogs/add-bug-dialog/add-bug-dialog.component';
import { AddProjectDialogComponent } from './navigation-header-dialogs/add-project-dialog/add-project-dialog.component';
import { AddQueryDialogComponent } from './navigation-header-dialogs/add-query-dialog/add-query-dialog.component';
import { AddTaskDialogComponent } from './navigation-header-dialogs/add-task-dialog/add-task-dialog.component';
import { AddTestDialogComponent } from './navigation-header-dialogs/add-test-dialog/add-test-dialog.component';
import { AddUserDialogComponent } from './navigation-header-dialogs/add-user-dialog/add-user-dialog.component';
import { AddReleaseDialogComponent } from './navigation-header-dialogs/add-release-dialog/add-release-dialog.component';
import { AddProductDialogComponent } from './navigation-header-dialogs/add-product-dialog/add-product-dialog.component';

@Component({
  selector: 'navigation-header',
  imports: [MatToolbar, MatButtonModule],
  templateUrl: './navigation-header.component.html',
  styleUrl: './navigation-header.component.scss'
})
export class NavigationHeaderComponent {
  readonly dialog = inject(MatDialog);
  
  addBug(): void {
    this.openDialog(AddBugDialogComponent);
  }
  addProject(): void{
    this.openDialog(AddProjectDialogComponent);
  }
  addQuery(): void{
    this.openDialog(AddQueryDialogComponent);
  }
  addTask(): void{
    this.openDialog(AddTaskDialogComponent);
  }
  addTest(): void{
    this.openDialog(AddTestDialogComponent);
  }
  addUser(): void{
    this.openDialog(AddUserDialogComponent);
  }
  addRelease(): void{
    this.openDialog(AddReleaseDialogComponent);
  }
  addProduct(): void{
    this.openDialog(AddProductDialogComponent);
  }

  private openDialog(DialogContent: any): void {
    const dialogRef = this.dialog.open(DialogContent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result:`, result);
    });
  }
}
