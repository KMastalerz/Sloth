import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { getLoginPageResolver } from './get-login-page.resolver';

describe('getLoginPageResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => getLoginPageResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
