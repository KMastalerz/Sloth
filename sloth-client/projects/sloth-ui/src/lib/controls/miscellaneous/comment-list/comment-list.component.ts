import { Component, input } from '@angular/core';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';
import { CommentComponent } from '../comment/comment.component';

@Component({
  selector: 'sl-comment-list',
  imports: [CommentComponent],
  templateUrl: './comment-list.component.html',
  styleUrl: './comment-list.component.scss'
})
export class CommentListComponent extends BaseFormControlComponent {
  commentKey = input.required<string>();
  commentIDKey = input.required<string>()
  commentDateKey = input.required<string>();
  commentedByKey = input.required<string>();
  commentedByUserNameKey = input.required<string>()
}
