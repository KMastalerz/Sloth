import { TestBed } from '@angular/core/testing';

import { SlothHttpService } from './sloth-http.service';

describe('SlothHttpService', () => {
  let service: SlothHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlothHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
