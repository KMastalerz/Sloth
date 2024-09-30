import { inject } from '@angular/core';
import { ResolveFn, Router } from '@angular/router';
import { UIService } from '@sloth-http';
import { PageSync } from '@sloth-ui';

export const pageResolver: ResolveFn<PageSync> = async (route, state) => {
  const services = inject(UIService);
  const pageSync = new PageSync();
  const router = inject(Router);

  const destination = route.routeConfig?.path;
  const results = await services.getWebPageAsync(destination!);

  if(results.success){
    const webPage = results.data;
    pageSync.pageConfig = webPage;
    return pageSync;
  } else if (results.responseCode === 400) {
    throw console.error(`Requested page: ${destination} was not found!`);
    }
    else {
    router.navigate(['no-service']);
    throw console.error('[pageResolver] Error:', results);
  }
};
