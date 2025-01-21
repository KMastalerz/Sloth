import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CacheAssigneeItem, CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CacheStatusItem, JobService } from 'sloth-http';

@Injectable({
  providedIn: 'root'
})
export class JobDataCacheService {
  private jobService = inject(JobService)

  jobTypes = new BehaviorSubject<string[]>([]);
  clients = new BehaviorSubject<CacheClientItem[]>([]);
  priorities = new BehaviorSubject<CachePriorityItem[]>([]);
  products = new BehaviorSubject<CacheProductItem[]>([]);
  functionalities = new BehaviorSubject<CacheFunctionalityItem[]>([]);
  statuses = new BehaviorSubject<CacheStatusItem[]>([]);
  assignees = new BehaviorSubject<CacheAssigneeItem[]>([]);

  constructor() { 
    this.initialize();
  }

  async initialize(): Promise<void> {
    var response = await this.jobService.listJobDataCacheAsync();
    if(response.success) {
      this.clients.next(response.data.clients);
      this.products.next(response.data.products);
      this.priorities.next(response.data.priorities);
      this.functionalities.next(response.data.functionalities);
      this.statuses.next(response.data.statuses);
    }
    this.jobTypes.next(["Bug", "Query", "Task"])
  }


  async listProductsWithClientIDAsync(clientID: string | null) {
    var response = await this.jobService.listProductsWithClientIDAsync(clientID)
    this.products.next(response.data);
  }

  async listFunctionalitiesWithProductIDAsync(productIDs: number[] | null) {
    var response = await this.jobService.listFunctionalitiesWithProductIDAsync(productIDs)
    this.functionalities.next(response.data);
  }
}
