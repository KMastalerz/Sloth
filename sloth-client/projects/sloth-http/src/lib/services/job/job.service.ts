import { Injectable } from '@angular/core';
import { BaseService } from '../../base/base-service.class';
import { ServiceReturnValue } from '../../dto/base/service-return-value.model';
import { ListJobDataCacheItem } from '../../dto/job/items/list-job-data-cache.item';
import { CreateJobParam } from '../../dto/job/params/create-job.param';
import { ListBugParam } from '../../dto/job/params/list-bug.param';
import { ListBugResponse } from '../../dto/job/items/list-bug.item';
import { GetAssignmentBugItem, GetBugItem, GetCommentBugItem } from '../../dto/job/items/get-bug.item';
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

  async deleteBugAsync(bugID: number): Promise<ServiceReturnValue<any>> {
    return await this.deleteAsync("DeleteBug", {bugID});
  }

  async claimBugAsync(bugID: number): Promise<ServiceReturnValue<GetAssignmentBugItem[]>> {
    return await this.postAsync("ClaimBug", {bugID}, {useQueryParams: true});
  }

  async abdandonBugAsync(bugID: number): Promise<ServiceReturnValue<GetAssignmentBugItem[]>> {
    return await this.postAsync("AbandonBug", {bugID}, {useQueryParams: true});
  }
}
