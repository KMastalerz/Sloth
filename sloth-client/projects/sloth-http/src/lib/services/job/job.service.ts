import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { CacheFunctionalityItem, CacheProductItem, ListJobDataCacheItem } from '../../dto/job/items/list-job-data-cache.item';
import { CreateQuickJobParam } from '../../dto/job/params/create-quick-job.param';
import { ListBugParam } from '../../dto/job/params/list-bug.param';
import { ListBugResponse } from '../../dto/job/items/list-bug.item';
import { GetBugItem, GetCommentBugItem } from '../../dto/job/items/get-bug.item';
import { AddJobCommentParam } from '../../dto/job/params/add-job-comment.param';


@Injectable({
  providedIn: 'root'
})
export class JobService extends BaseService {

  constructor() { 
    super("Job")
  }

  listJobDataCacheAsync(): Promise<ServiceReturnValue<ListJobDataCacheItem>> {
    return this.getAsync<ListJobDataCacheItem>("ListJobDataCache");
  }

  async createQuickJob(command: CreateQuickJobParam): Promise<ServiceReturnValue<any>> {    
    return await this.postAsync("CreateQuickJob", command, true);
  }

  async listProductsWithClientIDAsync(clientID: string| null): Promise<ServiceReturnValue<CacheProductItem[]>> {
    if(clientID) {
      return await this.getAsync("ListProductsWithClientID", {clientID}) ?? [];
    }
    return await this.getAsync("ListProductsWithClientID") ?? [];
  } 

  async listFunctionalitiesWithProductIDAsync(productIDs: number[] | null): Promise<ServiceReturnValue<CacheFunctionalityItem[]>> {
    return await this.getAsync("ListFunctionalitiesWithProductID", {productIDs}) ?? [];
  } 

  async listBugsAsync(command: ListBugParam): Promise<ServiceReturnValue<ListBugResponse>> {
    return await this.getAsync("ListBugs", command) ?? [];
  }

  async getBugAsync(bugID: number): Promise<ServiceReturnValue<GetBugItem | null>> {
    return await this.getAsync("GetBug", {bugID}) ?? [];
  }

  async addJobCommentAsync(command: AddJobCommentParam): Promise<ServiceReturnValue<GetCommentBugItem[]>> {
    return await this.postAsync("AddJobComment", command);
  }
}
