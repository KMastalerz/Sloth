import { ApplicationConfig, provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations'
import { routes } from './app.routes';
import { authInterceptor } from '@sloth-http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(), 
    provideRouter(routes, withComponentInputBinding(), withRouterConfig({
      paramsInheritanceStrategy:'always',
    }) ),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideAnimations(),
  ]
};
