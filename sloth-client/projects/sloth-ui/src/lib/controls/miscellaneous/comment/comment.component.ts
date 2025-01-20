import { DatePipe, NgClass } from '@angular/common';
import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { GetCommentBugItem } from 'sloth-http';
import { AuthStateService, User } from 'sloth-utilities';
import { BaseFormControlComponent } from '../../base-form-control.component';

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
    return this.user()?.userName === this.comment().commentedBy.userName
  })

  override ngOnInit(): void {
    super.ngOnInit();
    this.user.set(this.authStateService.user);
  }

  comment = input.required<GetCommentBugItem>();
}