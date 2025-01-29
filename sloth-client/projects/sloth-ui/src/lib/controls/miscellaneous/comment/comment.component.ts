import { DatePipe, NgClass } from '@angular/common';
import { Component, computed, inject, input, model, OnInit, signal } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';
import { AuthStateService, MarkdownService, User } from 'sloth-utilities';

@Component({
  selector: 'sl-comment',
  imports: [DatePipe, NgClass],
  templateUrl: './comment.component.html',
  styleUrl: './comment.component.scss'
})
export class CommentComponent implements OnInit {
  private readonly authStateService = inject(AuthStateService);
  private readonly markdownService = inject(MarkdownService);
  private user = signal<User | undefined | null>(undefined)
  protected isAuthor = computed<boolean>(() => {
    if(!this.user()) return false;
    return this.user()?.userName === this.commentedByUserName();
  })

  comment = model<any | null | undefined>(undefined);
  commentText = model<any | null | undefined>(undefined);
  commentID = model<number | null | undefined>(undefined)
  commentDate = model<Date | null | undefined>(undefined);
  commentedBy = model<string | null | undefined>(undefined);
  commentedByUserName = model<string | null | undefined>(undefined);

  commentTextKey = input<string | null | undefined>(undefined);
  commentIDKey = model<string | null | undefined>(undefined)
  commentDateKey = input<string | null | undefined>(undefined);
  commentedByKey = input<string | null | undefined>(undefined);
  commentedByUserNameKey = input<string | null | undefined>(undefined);

  sanitizedValue = signal<SafeHtml>('');
  
  async ngOnInit(): Promise<void> {
    this.user.set(this.authStateService.user);
    
    this.commentText.subscribe(async (value)=> {
      this.sanitizedValue.set(await this.markdownService.sanitizeValue(value));
    })

    if(this.comment()) {
      if(this.commentTextKey()) {
        this.commentText.set(this.comment()[this.commentTextKey()!])
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