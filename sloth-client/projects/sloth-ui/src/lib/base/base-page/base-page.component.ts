import { Component, inject, input } from '@angular/core';
import { PageSync } from '../../page-sync/page-sync';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'sl-base-page',
  standalone: true,
  template:''
})
export class BasePage {
  protected router = inject(Router);
  protected activatedRoute = inject(ActivatedRoute);
  pageSync = input.required<PageSync>();
}
