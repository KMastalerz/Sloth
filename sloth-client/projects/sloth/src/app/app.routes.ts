import { Routes } from '@angular/router';
import { MainComponent } from './core/main/main.component';
import { getMainPage } from './app.resolvers';

export const routes: Routes = [
    {   
        path: '', 
        component: MainComponent,
        resolve: {
            page: getMainPage,
        }
    },
];
