import { inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CacheAssigneeItem, CacheClientItem, CacheFunctionalityItem, CachePriorityItem, CacheProductItem, CacheStatusItem, JobService } from 'sloth-http';

@Injectable()
export class JobDataCacheService {
  private jobService = inject(JobService)
  private _clients: CacheClientItem[] = [];
  private _priorities: CachePriorityItem[] = [];
  private _products: CacheProductItem[] = [];
  private _functionalities: CacheFunctionalityItem[] = [];
  private _statuses: CacheStatusItem[] = [];
  private _assignees: CacheAssigneeItem[] = [];

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
      this.priorities.next(response.data.priorities);
      this.products.next(response.data.products);
      this.functionalities.next(response.data.functionalities);
      this.statuses.next(response.data.statuses);

      this._clients = response.data.clients;
      this._priorities = response.data.priorities;
      this._products = response.data.products;
      this._functionalities = response.data.functionalities;
      this._statuses = response.data.statuses;
    }
    this.jobTypes.next(["Bug", "Query", "Task"])
  }

  reset() {
    this.clients.next(this._clients);
    this.priorities.next(this._priorities);
    this.products.next(this._products);
    this.functionalities.next(this._functionalities);
    this.statuses.next(this._statuses);
  }

  listProductsWithClientIDAsync(clientID: string | null) {
    if(clientID) {
      const client = this._clients.filter(c=> c.clientID === clientID);
      this.products.next(client[0].products);
    }
    else this.products.next(this._products);
  }

  listFunctionalitiesWithProductIDAsync(productIDs: number[] | null) {
    if(productIDs) {
      const functionalities = this._functionalities.filter(f => productIDs.some(p => p === f.productID) || f.productID == null)
      this.functionalities.next(functionalities);
    }
    else this.functionalities.next(this._functionalities);
  }
}
