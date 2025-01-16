import { Component, computed, input, model, output, signal } from '@angular/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { PaginatorEvent } from '../../../models/paginator-event.model';

@Component({
  selector: 'sl-paginator',
  imports: [MatSelectModule, MatButtonModule, MatIcon],
  templateUrl: './paginator.component.html',
  styleUrl: './paginator.component.scss'
})
export class PaginatorComponent {
  pageSizes = input<number[]>([20,50,100]);
  pageSize = model<number>(50);
  totalCount = input<number>(0);
  changed = output<PaginatorEvent>();

  currentPage = signal<number>(1);
  displayedRecords = computed<string>(()=> {
    let from = this.currentPage() * this.pageSize() - (this.pageSize() - 1);
    let to = this.currentPage() * this.pageSize();
    if(from < 0) from = 0;
    if(to > this.totalCount()) to = this.totalCount();
    return`${from} — ${to} of ${this.totalCount()}`
  });
  lastPage = computed(()=> Math.ceil(this.totalCount() / this.pageSize()))
  firstPageDisabled = computed<boolean>(()=> this.currentPage() <= 1 || this.lastPage() <= 1);
  previousPageDisabled = computed<boolean>(()=> this.currentPage() <= 1 || this.lastPage() <= 1);
  nextPageDisabled = computed<boolean>(()=> this.currentPage() == this.lastPage());
  lastPageDisabled = computed<boolean>(()=> this.currentPage() == this.lastPage());

  emitChange(): void {
    const pageEvent: PaginatorEvent = {
      currentPage: this.currentPage(),
      pageSize: this.pageSize()
    }
    this.changed.emit(pageEvent);
  }

  goToFirstPage(): void {
    this.currentPage.set(1);
    this.emitChange();
  }

  goToPreviousPage(): void {
    this.currentPage.set(this.currentPage() - 1);
    this.emitChange();
  }

  goToNextPage(): void {
    this.currentPage.set(this.currentPage() + 1);
    this.emitChange();
  }

  goToLastPage(): void {
    this.currentPage.set(this.lastPage());
    this.emitChange();
  }

}
