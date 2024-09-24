import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';

import { UIService } from '@sloth-http';

import { DynamicForm } from '../../models/dynamic-form';
import { FormBuilderService } from '../../services/form-builder/form-builder.service';
import { DynamicDirectoryService } from '../../directories/dynamic-directory/dynamic-directory.service';

export const dynamicFormResolver: ResolveFn<DynamicForm> = async (route, state) => {
  const dirService = inject(DynamicDirectoryService);
  const formBuilder = inject(FormBuilderService);
  const services = inject(UIService);


  const destination = route.routeConfig?.path;

  if (!destination) {
    throw console.error('[DynamicFormResolver] No destination provided');
  }

  const page = dirService.getPageID(destination);

  if (!page) {
    throw console.error('[DynamicFormResolver] No page found');
  }

  const results = await services.getWebPageAsync(page);

  if(results.success){
      const config = results.data;
      const pageForm = formBuilder.buildForm(config);

      return {
        pageForm: pageForm,
        config: config
      } as DynamicForm;

  } else {
      throw console.error('[DynamicFormResolver] Error: ' + results.error);
  }
};
