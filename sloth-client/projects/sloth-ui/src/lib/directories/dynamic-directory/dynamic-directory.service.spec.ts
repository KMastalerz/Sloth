import { TestBed } from '@angular/core/testing';

import { DynamicDirectoryService } from './dynamic-directory.service';

describe('DynamicDirectoryService', () => {
  let service: DynamicDirectoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DynamicDirectoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
