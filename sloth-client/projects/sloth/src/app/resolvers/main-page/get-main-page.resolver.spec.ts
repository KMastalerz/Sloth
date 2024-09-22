import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { getMainPageResolver } from './get-main-page.resolver';

describe('getMainPageResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => getMainPageResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
