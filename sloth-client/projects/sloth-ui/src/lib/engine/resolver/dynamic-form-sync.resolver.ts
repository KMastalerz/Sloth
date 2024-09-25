import { ResolveFn } from '@angular/router';
import { DynamicFormSync } from '../dynamic-form-sync';
import { inject } from '@angular/core';
import { UIService } from '@sloth-http';
import { FormBuilderService } from '../form-builder/form-builder.service';
import { DynamicDirectoryService } from '../directories/dynamic-directory/dynamic-directory.service';

export const dynamicFormSyncResolver: ResolveFn<DynamicFormSync> = async (route, state) => {
  const dirService = inject(DynamicDirectoryService);
  const formBuilder = inject(FormBuilderService);
  const services = inject(UIService);

  const destination = route.routeConfig?.path;
  const page = dirService.getPageID(destination!);
  const results = await services.getWebPageAsync(page ?? destination!);

  if(results.success){
      const webPage = results.data;
      console.log('[dynamicFormSyncResolver] webPage:', webPage);
      
      return formBuilder.buildFormSync(webPage);
  } else {
      throw console.error('[dynamicFormSyncResolver] Error:' + results.error);
  }
};
