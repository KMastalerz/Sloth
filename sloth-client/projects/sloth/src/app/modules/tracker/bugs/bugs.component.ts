import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';

@Component({
    selector: 'app-bugs',
    imports: [MatTableModule],
    templateUrl: './bugs.component.html',
    styleUrl: './bugs.component.scss'
})
export class BugsComponent implements OnInit {
    // protected data: = [];

    ngOnInit(): void {

    }


}
