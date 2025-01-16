import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, inject, OnInit, signal, viewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BehaviorSubject } from 'rxjs';
import { JobService, ListBugItem } from 'sloth-http';
import { PaginatorComponent, PaginatorEvent, RegularLinkComponent, TagComponent } from 'sloth-ui';
import { StringService } from 'sloth-utilities';

@Component({
    selector: 'app-bugs',
    imports: [MatTableModule, MatButtonModule, MatMenuModule, MatPaginatorModule, DatePipe, TagComponent, RegularLinkComponent, PaginatorComponent],
    templateUrl: './bugs.component.html',
    styleUrl: './bugs.component.scss'
})
export class BugsComponent implements OnInit {
    private readonly jobService = inject(JobService);
    private readonly stringService = inject(StringService)
    private data = new BehaviorSubject<ListBugItem[]>([]);
    protected pageSizes = signal<number[]>([20,50,100]);
    protected pageSize = signal<number>(50);
    protected currentPage = 1;
    protected totalCount = signal<number>(0);
    // protected data: = [];
    displayedColumns = signal<string[]>([
        'BugID', 
        'InquiryNumber', 
        'Client',
        'Header', 
        'Priority', 
        'Status', 
        'CreatedDate', 
        'Products', 
        'Functionalities', 
        'Claim'
      ]);

    expandedElement: ListBugItem | null = null;
    dataSource = new MatTableDataSource<ListBugItem>();

    constructor(){
        this.data
            .pipe(takeUntilDestroyed())
            .subscribe(d=> {
                this.dataSource.data = d;
        });
    }

    async ngOnInit(): Promise<void> {
        this.loadData();
    }

    async loadData(): Promise<void> {
        const result = await this.jobService.listBugsAsync({pageID: this.currentPage, takeCount: this.pageSize()});
        if(result.success) {
            console.log('total records:', result.data.totalCount);
            
            result.data.bugs.forEach(d=>d.functionalities?.forEach(
                    f=>f.tagColor = this.stringService.toInitialLowerCase(f.tagColor)));
            this.data.next(result.data.bugs);
            this.totalCount.set(result.data.totalCount);
        }
    }

    claimBug(bug: ListBugItem): void {
        // Implement your claim logic here
        console.log('Claiming bug:');
    }

    onPaginateChange(event: PaginatorEvent){
        this.currentPage = event.currentPage;
        this.pageSize.set(event.pageSize); 
        this.loadData(); // Reload data based on new pagination
    }
}
