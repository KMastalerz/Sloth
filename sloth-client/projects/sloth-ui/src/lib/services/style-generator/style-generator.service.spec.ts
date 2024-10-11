import { TestBed } from '@angular/core/testing';

import { StyleGeneratorService } from './style-generator.service';

describe('StyleGeneratorService', () => {
  let service: StyleGeneratorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StyleGeneratorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
