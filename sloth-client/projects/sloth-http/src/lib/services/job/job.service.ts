import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { ListJobDataCacheItem } from '../../dto/job/items/list-job-data-cache.item';
import { CreateJobParam } from '../../dto/job/params/create-job.param';
import { ListBugParam } from '../../dto/job/params/list-bug.param';
import { ListBugResponse } from '../../dto/job/items/list-bug.item';
import { GetAssignmentBugItem, GetBugItem, GetCommentBugItem } from '../../dto/job/items/get-bug.item';
import { AddJobCommentParam } from '../../dto/job/params/add-job-comment.param';
import { SaveBugParam } from '../../dto/job/params/save-bug.param';
import { GetUserCountersItem } from '../../dto/job/items/get-user-counters.item';


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

  async createJob(command: CreateJobParam): Promise<ServiceReturnValue<any>> {    
    return await this.postAsync("CreateJob", command, {useFormData: true});
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

  async deleteJobAsync(jobID: number): Promise<ServiceReturnValue<any>> {
    return await this.deleteAsync("DeleteJob", {jobID});
  }

  async claimJobAsync(jobID: number): Promise<ServiceReturnValue<GetAssignmentBugItem[]>> {
    return await this.postAsync("ClaimJob", {jobID}, {useQueryParams: true});
  }

  async abdandonJobAsync(jobID: number): Promise<ServiceReturnValue<GetAssignmentBugItem[]>> {
    return await this.postAsync("AbandonJob", {jobID}, {useQueryParams: true});
  }

  async SaveBugAsync(command: SaveBugParam): Promise<ServiceReturnValue<GetBugItem>> {
    return await this.postAsync("SaveBug", command);
  }

  async GetUserCountersAsync(): Promise<ServiceReturnValue<GetUserCountersItem>> {
    return await this.getAsync("GetUserCounters");
  }
}
