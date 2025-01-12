import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { ListJobDataCacheItem } from '../../dto/job/items/list-job-data-cache.item';
import { CreateQuickJobParam } from '../../dto/job/params/create-quick-job.param';

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
}
