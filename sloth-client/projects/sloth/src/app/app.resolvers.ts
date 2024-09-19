import { ResolveFn } from "@angular/router";
import { inject } from "@angular/core";

import { UIService } from "@sloth-http";

import { WebPage } from "../../../sloth-http/src/lib/models/ui-service/page.model";

export  const getMainPage: ResolveFn<WebPage | null> = async (activedRoute, routeState) => {
    const services = inject(UIService);

    return await services.getMainWebPage() ?? null;
}
 