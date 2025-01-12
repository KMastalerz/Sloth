import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, provideNativeDateAdapter } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

import { routes } from './app.routes';
import { authInterceptor } from './core/interceptors/auth/auth.interceptor';

export const locale = navigator.language || navigator.languages?.[0] || 'pl-PL';
console.log('Locale:', locale);
console.log('Navigator:', navigator);

function guessLocaleDateTimeFormat(locale: string) {
  // Pick a date/time that has distinct values for day vs. month, and includes hour info.
  // For example: January 12, 2025, 12:00
  const sampleDate = new Date(2025, 0, 12, 12, 0);

  // Use short date & time styles (often the user’s typical choice):
  const dtf = new Intl.DateTimeFormat(locale, {
    dateStyle: 'short',
    timeStyle: 'short',
  });

  // Break down the output into parts:
  // e.g. "1/12/25, 12:00 PM" => [
  //   { type: 'month', value: '1' },
  //   { type: 'literal', value: '/' },
  //   { type: 'day', value: '12' },
  //   { type: 'literal', value: '/' },
  //   { type: 'year', value: '25' },
  //   { type: 'literal', value: ', ' },
  //   { type: 'hour', value: '12' },
  //   { type: 'literal', value: ':' },
  //   { type: 'minute', value: '00' },
  //   { type: 'literal', value: ' ' },
  //   { type: 'dayPeriod', value: 'PM' }
  // ]
  const parts = dtf.formatToParts(sampleDate);
  const resolved = dtf.resolvedOptions();

  // Indexes where day, month, year, dayPeriod occur in the parts array
  const dayIndex = parts.findIndex(p => p.type === 'day');
  const monthIndex = parts.findIndex(p => p.type === 'month');
  const yearIndex = parts.findIndex(p => p.type === 'year');
  const dayPeriodIndex = parts.findIndex(p => p.type === 'dayPeriod');

  // Determine date format (month-first vs day-first):
  let dateFormat = 'DD/MM/YYYY'; // default
  if (monthIndex < dayIndex) {
    // If the "month" occurs before the "day" part, assume "MM/DD/YYYY"
    dateFormat = 'MM/DD/YYYY';
  } else {
    // Else assume "DD/MM/YYYY"
    dateFormat = 'DD/MM/YYYY';
  }

  // Determine time format (12-hour vs. 24-hour) from hourCycle or presence of dayPeriod
  let timeFormat = 'HH:mm'; // 24-hour default
  if (resolved.hourCycle === 'h12' || dayPeriodIndex !== -1) {
    // If the user uses a 12-hour clock, or if dayPeriod is present, 
    // we can assume "hh:mm A" for Moment
    timeFormat = 'hh:mm A';
  }

  // Return the final date/time formats
  return { dateFormat, timeFormat };
}

export function createDateTimeFormats(locale: string) {
  const { dateFormat, timeFormat } = guessLocaleDateTimeFormat(locale);

  return {
    parse: {
      dateInput: dateFormat,
      timeInput: timeFormat,
    },
    display: {
      // Adjust as you wish:
      dateInput: dateFormat,
      timeInput: timeFormat,
      timeOptionLabel: timeFormat,
      monthYearLabel: 'MMMM YYYY',
      dateA11yLabel: 'LL',         
      monthYearA11yLabel: 'MMMM YYYY',
      timeA11yLabel: timeFormat   
    }
  };
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes, withComponentInputBinding(), withRouterConfig({
      paramsInheritanceStrategy:'always',
    })),
    provideAnimations(),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideNativeDateAdapter(),
    // Provide your chosen locale to Angular Material
    { provide: MAT_DATE_LOCALE, useValue: locale },

    // Use the Moment adapter with the provided locale
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    // Provide dynamic date/time formats via your factory:
    {
      provide: MAT_DATE_FORMATS,
      useFactory: createDateTimeFormats,
      deps: [MAT_DATE_LOCALE]
    }
  ]
};
