import { Component, inject } from '@angular/core';
import { MatToolbar } from '@angular/material/toolbar';
import { MatDialog } from '@angular/material/dialog';
import { AddBugDialogComponent } from './navigation-header-dialogs/add-bug-dialog/add-bug-dialog.component';
import { FlatButtonComponent, HeaderLinkComponent } from 'sloth-ui';


@Component({
  selector: 'navigation-header',
  imports: [MatToolbar, FlatButtonComponent, HeaderLinkComponent],
  templateUrl: './navigation-header.component.html',
  styleUrl: './navigation-header.component.scss'
})
export class NavigationHeaderComponent {
  readonly dialog = inject(MatDialog);
  
  addBug(): void {
    const dialogRef = this.dialog.open(AddBugDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result:`, result);
    });
  }
}
