import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSelectModule } from '@angular/material/select';
import { ListSelectComponent, ListSelectItem, MarkupInputComponent, SectionComponent, TextInputComponent, ToggleListComponent, ToggleListItem } from 'sloth-ui';

@Component({
  selector: 'app-add-bug-dialog',
  imports: [MatDialogTitle,
    MatDialogActions,
    MatDialogContent,
    MatFormFieldModule,
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatButtonToggleModule,
    MatSelectModule,
    TextInputComponent,
    MarkupInputComponent,
    SectionComponent,
    ToggleListComponent, 
    ListSelectComponent],
  templateUrl: './add-bug-dialog.component.html',
  styleUrl: './add-bug-dialog.component.scss'
})
export class AddBugDialogComponent {
  readonly dialogRef = inject(MatDialogRef<AddBugDialogComponent>);

  priotities = signal<ToggleListItem[]>([
    {
      value: 'lowest',
      display: 'Lowest',
      class: 'lowest'
    },
    {
      value: 'low',
      display: 'Low',
      class: 'low'
    },
    {
      value: 'medium',
      display: 'Medium',
      class: 'medium'
    },
    {
      value: 'high',
      display: 'High',
      class: 'high'
    },
    {
      value: 'critical',
      display: 'Critical',
      class: 'critical'
    }
    ,
    {
      value: 'regular',
      display: 'Regular'
    }
  ])
  projects = signal<ListSelectItem[]>([
    {
      value: "mw+",
      display: "MASWeb+"
    },
    {
      value: "ex",
      display: "MASEX"
    },
    {
      value: "mmm",
      display: "Mastermind Monitoring"
    },
    {
      value: "mmb",
      display: "Mastermind Business"
    },
    {
      value: "mw",
      display: "MASWeb"
    }
  ]);
  types = signal<ListSelectItem[]>([
    {
      value: "Bug"
    }, 
    {
      value: "Query"
    }, 
    {
      value: "Task"
    }, 
    {
      value: "Project"
    }
  ]);
  bugHeader = signal<string>("");
  bugDescription = signal<string>("");
  bugPriority = signal<string>("");
  bugProjects = signal<string[]>([]);

  onCloseDialog(): void {
    this.dialogRef.close();
  }

  onSaveBug(): void {
    console.log('Clicked Add');
    
    this.dialogRef.close({
      header: this.bugHeader(),
      description: this.bugDescription(),
      priority: this.bugPriority(),
      projects: this.bugProjects()
    });
  }
}
