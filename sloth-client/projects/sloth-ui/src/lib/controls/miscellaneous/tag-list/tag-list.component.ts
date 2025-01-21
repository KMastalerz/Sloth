import { Component, input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TagComponent } from '../tag/tag.component';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-tag-list',
  imports: [TagComponent, ReactiveFormsModule],
  templateUrl: './tag-list.component.html',
  styleUrl: './tag-list.component.scss'
})
export class TagListComponent extends BaseFormControlComponent {
  formControlNames = input<string | null | undefined>(undefined);
  backgroundControlName = input<string | null | undefined>(undefined);
  tooltipControlName = input<string | null | undefined>(undefined);
}
