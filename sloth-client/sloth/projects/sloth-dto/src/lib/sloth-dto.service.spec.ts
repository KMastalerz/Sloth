import { TestBed } from '@angular/core/testing';

import { SlothDtoService } from './sloth-dto.service';

describe('SlothDtoService', () => {
  let service: SlothDtoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlothDtoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
