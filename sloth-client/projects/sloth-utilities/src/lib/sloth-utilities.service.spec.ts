import { TestBed } from '@angular/core/testing';

import { SlothUtilitiesService } from './sloth-utilities.service';

describe('SlothUtilitiesService', () => {
  let service: SlothUtilitiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlothUtilitiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
