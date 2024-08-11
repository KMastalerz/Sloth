import { TestBed } from '@angular/core/testing';

import { SlothUiService } from './sloth-ui.service';

describe('SlothUiService', () => {
  let service: SlothUiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SlothUiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
