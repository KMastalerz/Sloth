import { TestBed } from '@angular/core/testing';

import { StringUtilityService } from './string-utility.service';

describe('StringUtilityService', () => {
  let service: StringUtilityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StringUtilityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
