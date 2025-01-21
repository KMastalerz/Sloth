import { DatePipe, NgClass } from '@angular/common';
import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { GetCommentBugItem } from 'sloth-http';
import { AuthStateService, User } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control/base-form-control.component';

@Component({
  selector: 'sl-comment',
  imports: [DatePipe, NgClass],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent extends BaseFormControlComponent implements OnInit {
  private readonly authStateService = inject(AuthStateService);
  private user = signal<User | undefined | null>(undefined)
  protected isAuthor = computed<boolean>(() => {
    if(!this.user()) return false;
    return this.user()?.userName === this.commentedByUserName();
  })

  comment = model<any | null | undefined>(undefined);
  commentID = model<number | null | undefined>(undefined)
  commentDate = model<Date | null | undefined>(undefined);
  commentedBy = model<string | null | undefined>(undefined);
  commentedByUserName = model<string | null | undefined>(undefined);

  commentKey = input<string | null | undefined>(undefined);
  commentIDKey = model<string | null | undefined>(undefined)
  commentDateKey = input<string | null | undefined>(undefined);
  commentedByKey = input<string | null | undefined>(undefined);
  commentedByUserNameKey = input<string | null | undefined>(undefined);

  override ngOnInit(): void {
    super.ngOnInit();
    this.user.set(this.authStateService.user);

    if(this.comment()) {
      if(this.commentKey()) {
        this.value.set(this.comment()[this.commentKey()!])
      }
      if(this.commentDateKey()) {
        this.commentDate.set(this.comment()[this.commentDateKey()!])
      }
      if(this.commentedByKey()) {
        this.commentedBy.set(this.comment()[this.commentedByKey()!])
      }
      if(this.commentedByUserNameKey()) {
        this.commentedByUserName.set(this.comment()[this.commentedByUserNameKey()!])
      }
      if(this.commentIDKey()) {
        this.commentID.set(this.comment()[this.commentIDKey()!])
      }
    }
  }
}