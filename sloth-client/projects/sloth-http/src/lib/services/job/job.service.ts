import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { ListJobDataCacheItem } from '../../dto/job/list-job-data-cache.item';

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
}
