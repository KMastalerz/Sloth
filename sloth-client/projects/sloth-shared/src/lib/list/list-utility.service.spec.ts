import { TestBed } from '@angular/core/testing';

import { ListUtilityService } from './list-utility.service';

describe('ListUtilityService', () => {
  let service: ListUtilityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListUtilityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
