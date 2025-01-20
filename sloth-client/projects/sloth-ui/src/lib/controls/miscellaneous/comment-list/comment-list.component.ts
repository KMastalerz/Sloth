import { Component } from '@angular/core';
import { BaseFormControlComponent } from '../../base-form-control.component';
import { CommentComponent } from '../comment/comment.component';

@Component({
  selector: 'sl-comment-list',
  imports: [CommentComponent],
  templateUrl: './comment-list.component.html',
  styleUrl: './comment-list.component.scss'
})
export class CommentListComponent extends BaseFormControlComponent {}
