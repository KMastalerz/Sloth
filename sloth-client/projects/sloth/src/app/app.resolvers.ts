import { ResolveFn } from "@angular/router";
import { inject } from "@angular/core";

import { UIService, WebPage } from "@sloth-http";

export  const getMainPage: ResolveFn<WebPage | null> = async (activedRoute, routeState) => {
    const services = inject(UIService);
    const results = await services.getMainWebPage();
    return results ?? null;
}
 