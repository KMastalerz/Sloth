import { TestBed } from '@angular/core/testing';

import { DynamicRegistrationService } from './dynamic-registration.service';

describe('DynamicRegistrationService', () => {
  let service: DynamicRegistrationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DynamicRegistrationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
