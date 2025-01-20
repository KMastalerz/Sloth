import { Component, forwardRef, input } from '@angular/core';
import { NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { TagComponent } from '../tag/tag.component';
import { BaseFormControlComponent } from '../../base-form-control.component';

@Component({
  selector: 'sl-tag-list',
  imports: [TagComponent, ReactiveFormsModule],
  templateUrl: './tag-list.component.html',
  styleUrl: './tag-list.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TagListComponent),
      multi: true
    }
  ]
})
export class TagListComponent extends BaseFormControlComponent {
  formControlNames = input<string | null | undefined>(undefined);
  backgroundControlName = input<string | null | undefined>(undefined);
  tooltipControlName = input<string | null | undefined>(undefined);
}
