import { Component, input, model, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TagComponent } from '../tag/tag.component';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-tag-list',
  imports: [TagComponent, ReactiveFormsModule],
  templateUrl: './tag-list.component.html',
  styleUrl: './tag-list.component.scss'
})
export class TagListComponent extends BaseFormControlComponent implements OnInit{
  tags =  model<any[]>();
  tagKey =  input<string | null>(null);
  tooltipKey =  input<string | null>(null);
  backgroundKey =  input<string| null>(null);

  tooltipPosition = input<'above' | 'below' | 'left' | 'right'>('below');
  fallbackValue = input<string | null | undefined>(undefined);

  override ngOnInit(): void {
    super.ngOnInit();

    if(this.formArray()) {
      this.tags.set(this.formArray()?.value);
    } 
  }
}
