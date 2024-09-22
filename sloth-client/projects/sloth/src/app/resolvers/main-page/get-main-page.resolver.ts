import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { UIService, WebPage } from '@sloth-http';

export const getMainPageResolver: ResolveFn<WebPage> = async (route, state) => {
  const services = inject(UIService);
  const results = await services.getMainWebPage();
  if(results.success){
      return results.data;
  } else {
      throw console.error(results.error);
  }
};
