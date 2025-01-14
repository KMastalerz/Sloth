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
  clients = new BehaviorSubject<ListSelectItem[]>([]);
  products = new BehaviorSubject<ListSelectItem[]>([]);
  jobPriorities = new BehaviorSubject<ListSelectItem[]>([]);

  
  constructor() { 
    this.initialize();
  }

  async initialize(): Promise<void> {
    var response = await this.jobService.listJobDataCacheAsync();
    if(response.success) {
      this.clients.next(response.data.clients);
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
      {
        value: "Task"
      }
    ])
  }


  async listProductsWithClientIDAsync(clientID: string | null) {
    var response = await this.jobService.listProductsWithClientIDAsync({clientID})
    this.products.next(response.data.products);
  }
}
