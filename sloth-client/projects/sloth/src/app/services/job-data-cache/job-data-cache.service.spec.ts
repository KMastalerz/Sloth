import { TestBed } from '@angular/core/testing';

import { JobDataCacheService } from './job-data-cache.service';

describe('JobDataCacheService', () => {
  let service: JobDataCacheService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobDataCacheService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
