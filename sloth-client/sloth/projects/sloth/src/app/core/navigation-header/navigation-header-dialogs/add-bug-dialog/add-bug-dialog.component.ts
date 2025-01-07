import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogTitle, MatDialogActions, MatDialogContent, MatFormFieldModule, FormsModule, MatButtonModule, MatInputModule, MatButtonToggleModule, MatSelectModule],
  templateUrl: './add-bug-dialog.component.html',
  styleUrl: './add-bug-dialog.component.scss'
})
export class AddBugDialogComponent {
  readonly dialogRef = inject(MatDialogRef<AddBugDialogComponent>);

  projects = signal<string[]>(['MASweb+', 'EX', 'MMM', 'MMB', 'MASweb']);
  types = signal<string[]>(['Bug', 'RTS']);
  bugHeader = signal<string>("");
  bugDescription = signal<string>("");
  bugPriority = signal<string>("");
  bugProjects = signal<string[]>([]);

  onCloseDialog(): void {
    this.dialogRef.close();
  }

  onSaveBug(): void {
    this.dialogRef.close({
      header: this.bugHeader(),
      description: this.bugDescription(),
      priority: this.bugPriority(),
      projects: this.bugProjects()
    });
  }
}
