import { animate, state, style, transition, trigger } from '@angular/animations';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, inject, OnInit, signal, viewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { BehaviorSubject } from 'rxjs';
import { JobService, ListBugItem, ListBugParam } from 'sloth-http';

@Component({
    selector: 'app-bugs',
    imports: [MatTableModule, MatIcon, MatButtonModule, MatMenuModule, MatPaginatorModule, DatePipe],
    templateUrl: './bugs.component.html',
    styleUrl: './bugs.component.scss',
    animations: [
        trigger('detailExpand', [
          state('collapsed,void', style({height: '0px', minHeight: '0'})),
          state('expanded', style({height: '*'})),
          transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
        ]),
    ],
})
export class BugsComponent implements OnInit, AfterViewInit {
    private readonly jobService = inject(JobService);
    private param: ListBugParam;
    private data = new BehaviorSubject<ListBugItem[]>([]);
    protected pageSizes = signal<number[]>([20,50,100]);
    // protected data: = [];
    displayedColumns: string[] = [
        'BugID', 
        'InquiryNumber', 
        'Header', 
        'Priority', 
        'Status', 
        'CreatedDate', 
        'Products', 
        'Functionalities', 
        'expander',
        'Claim'
      ];

    expandedElement: ListBugItem | null = null;
    paginator = viewChild('paginator', { read: MatPaginator });
    dataSource = new MatTableDataSource<ListBugItem>();

    constructor(){
        this.param = {
            pageID: 1,
            takeCount: 50
        };

        this.data
            .pipe(takeUntilDestroyed())
            .subscribe(d=> {
                console.log('new data', d);
                this.dataSource.data = d;
        });
    }

    async ngOnInit(): Promise<void> {
        this.loadData();
    }

    async loadData(): Promise<void> {
        const result = await this.jobService.listBugsAsync({pageID: 1, takeCount: 50});
        if(result.success) {
            this.data.next(result.data);
        }
    }

    ngAfterViewInit(): void {
        this.dataSource.paginator = this.paginator() ?? null;
    }
    
    claimBug(bug: ListBugItem): void {
        // Implement your claim logic here
        console.log('Claiming bug:');
    }

    onPaginateChange(event: PageEvent){
        this.loadData()
        console.log(JSON.stringify("Current page index: " + event.pageIndex));
        console.log(JSON.stringify("Current page size: " + event.pageSize));
    }


}
