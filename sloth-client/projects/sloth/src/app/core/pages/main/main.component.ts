import { Component, signal } from '@angular/core';
import { BasePage, DynamicFormComponent} from '@sloth-ui';
import { interval, map } from 'rxjs';

@Component({
  selector: 'sl-main',
  standalone: true,
  imports: [DynamicFormComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends BasePage {
  bugCount = signal<number>(0);
  projectCount = signal<number>(0);
  taskCount = signal<number>(0);
  unreadMessageCount = signal<number>(0);

  constructor(){
    super();
    this.getBugCount();
    this.getProjectCount();
    this.getTaskCount();
    this.getUnreadMessageCount();
  }

  async getBugCount(): Promise<void> {
    interval(5000)
      .pipe(map(() => Math.floor(Math.random() * 151)))
      .subscribe((randomNumber) => {
        this.bugCount.set(randomNumber); 
      });
  }
  
  async getProjectCount(): Promise<void> {
    interval(5000)
      .pipe(map(() => Math.floor(Math.random() * 151)))
      .subscribe((randomNumber) => {
        this.projectCount.set(randomNumber); 
      });
  }
  
  async getTaskCount(): Promise<void> {
    interval(5000)
      .pipe(map(() => Math.floor(Math.random() * 151)))
      .subscribe((randomNumber) => {
        this.taskCount.set(randomNumber); 
      });
  }
  
  async getUnreadMessageCount(): Promise<void> {
    interval(5000)
      .pipe(map(() => Math.floor(Math.random() * 151)))
      .subscribe((randomNumber) => {
        this.unreadMessageCount.set(randomNumber); 
      });
  }
}

