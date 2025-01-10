import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { JobService } from 'sloth-http';
import { ListSelectItem, ToggleListItem } from 'sloth-utilities';

@Injectable({
  providedIn: 'root'
})
export class JobDataCacheService {
  private jobService = inject(JobService)

  quickJobTypes = new BehaviorSubject<ListSelectItem[]>([]);
  products = new BehaviorSubject<ListSelectItem[]>([]);
  jobPriorities = new BehaviorSubject<ListSelectItem[]>([]);

  async initialize(): Promise<void> {
    var response = await this.jobService.listJobDataCacheAsync();
    if(response.success) {
      this.products.next(response.data.products);
      this.jobPriorities.next(response.data.jobPriorities);
    }
    this.quickJobTypes.next([
      {
        value: "Bug"
      }, 
      {
        value: "Query"
      }, 
    ])
  }

  constructor() { 
    this.initialize();
  }
}
