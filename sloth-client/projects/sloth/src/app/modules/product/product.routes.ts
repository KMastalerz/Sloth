import { Routes } from "@angular/router";
import { ProductAddComponent } from "./product-add/product-add.component";
import { pageResolver } from "../../core/resolvers/page/page.resolver";

export const productRoutes: Routes = [
    {   
        path: 'product-add', 
        component: ProductAddComponent,
        resolve: {
            pageSync: pageResolver
        }
    },
];
