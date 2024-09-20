import { ResolveFn } from "@angular/router";
import { inject } from "@angular/core";

import { UIService, WebPage } from "@sloth-http";

export  const getMainPage: ResolveFn<WebPage | null> = async (activedRoute, routeState) => {
    const services = inject(UIService);
    const results = await services.getMainWebPage();
    if(results.success){
        return results.data;
    } else {
        console.error('[Main Page Resolver] Error:', results.error);
        return null;
    }
}
 