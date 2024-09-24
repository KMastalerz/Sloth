import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { dynamicFormResolver } from './dynamic-form.resolver';

describe('dynamicFormResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => dynamicFormResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
