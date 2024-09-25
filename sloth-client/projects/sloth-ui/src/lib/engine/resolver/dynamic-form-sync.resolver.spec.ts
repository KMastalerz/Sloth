import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { dynamicFormSyncResolver } from './dynamic-form-sync.resolver';

describe('dynamicFormSyncResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => dynamicFormSyncResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
