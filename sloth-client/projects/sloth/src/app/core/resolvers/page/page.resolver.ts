import { inject } from '@angular/core';
import { ResolveFn, Router } from '@angular/router';
import { UIService } from '@sloth-http';
import { StringUtilityService } from '@sloth-shared';
import { DynamicPageSync } from '@sloth-ui';

export const pageResolver: ResolveFn<DynamicPageSync> = async (route, state) => {
  const services = inject(UIService);
  const pageSync = new DynamicPageSync();
  const router = inject(Router);
  const stringUtility = inject(StringUtilityService);

  const destination = stringUtility.toCamelCase(route.routeConfig?.path)!;
  const results = await services.getWebPageAsync(destination!);

  if(results.success){
    const webPage = results.data;
    pageSync.pageConfig = webPage;
    // Build the form for page
    pageSync.buildForm(); 
    
    console.log('[pageResolver] PageSync:', pageSync);
    return pageSync;
  } else if (results.responseCode === 400) {
    throw console.error(`Requested page: ${destination} was not found!`);
    }
    else {
    router.navigate(['no-service']);
    throw console.error('[pageResolver] Error:', results);
  }
};
