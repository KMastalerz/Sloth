import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { UIService } from '@sloth-http';
import { DirectoryService } from '@sloth-shared';
import { PageSync } from '@sloth-ui';

export const pageResolver: ResolveFn<PageSync> = async (route, state) => {
  const services = inject(UIService);
  const directory = inject(DirectoryService);
  const pageSync = new PageSync();

  const destination = route.routeConfig?.path;
  const page = directory.getPageID(destination!);
  const results = await services.getWebPageAsync(page ?? destination!);

  if(results.success){
    const webPage = results.data;
    pageSync.pageConfig = webPage;
    pageSync.controlsConfig = webPage.webControls ?? [];
    return pageSync;
} else {
    throw console.error('[pageResolver] Error:' + results.error);
}
};
