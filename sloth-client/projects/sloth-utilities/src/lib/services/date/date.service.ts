import { Injectable } from '@angular/core';
import dayjs from 'dayjs';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  private locale: string = '';
  private dateTimeStandard: string = '';
  private dateTimeFull: string = '';
  private dateOnly: string = '';

  constructor() { 
    this.locale = navigator.language || navigator.languages?.[0] || 'pl-PL';
    this.guessLocaleDateTimeFormat(this.locale);
  }

  private guessLocaleDateTimeFormat(locale: string) {
    const sampleDate = new Date(1990, 0, 1, 13, 0, 0);
  
    const dtf = new Intl.DateTimeFormat(locale, {
      dateStyle: 'short',
      timeStyle: 'medium'
    });

    const parts = dtf.formatToParts(sampleDate);
    const resolved = dtf.resolvedOptions();

    let dateTimeStandard = '';
    let dateTimeFull = '';
    let dateOnly = '';
    let literalCounter = 0;

    parts.forEach(p=> {
      switch(p.type){
        case 'day':
          dateTimeStandard += "DD";
          dateTimeFull += "DD"; 
          dateOnly += "DD"; 
          break;
        case 'month':
          dateTimeStandard += "MM"; 
          dateTimeFull += "MM"; 
          dateOnly += "MM"; 
          break;
        case 'year':
          dateTimeStandard += "YYYY"; 
          dateTimeFull += "YYYY"; 
          dateOnly += "YYYY";
          break;
        case 'literal':
          literalCounter++;
          if(literalCounter === 3) {
            p.value = " ";
          }
          dateTimeStandard += literalCounter <= 4 ? p.value : "";
          dateTimeFull += p.value;
          dateOnly += literalCounter <= 2 ? p.value : "";
          break;
        case 'hour':
          dateTimeStandard += resolved.hour12 ? 'hh' : 'HH';
          dateTimeFull += resolved.hour12 ? 'hh' : 'HH';
          break;
        case 'minute':
          dateTimeStandard += 'mm';
          dateTimeFull += 'mm';
          break;
        case 'second':
          dateTimeFull += 'ss';
          break;
        case 'dayPeriod':
          dateTimeFull += p.value;
          break;
      }
    });
    
    this.dateTimeStandard = dateTimeStandard;
    this.dateTimeFull = dateTimeFull;
    this.dateOnly = dateOnly;
  }

  toString(value: Date | null | undefined, format: DateFormat): string {
    if(!value) return '';

    switch(format) {
      case DateFormat.dateTimeStandard: 
        return dayjs(value).format(this.dateTimeStandard);
      case DateFormat.dateTimeFull: 
        return dayjs(value).format(this.dateTimeFull);
      case DateFormat.dateOnly: 
        return dayjs(value).format(this.dateOnly);
    }
  }

  maxLength(format: DateFormat): number {
    switch(format) {
      case DateFormat.dateTimeStandard: 
        return this.dateTimeStandard.length;
      case DateFormat.dateTimeFull: 
        return this.dateTimeFull.length;
      case DateFormat.dateOnly: 
        return this.dateOnly.length;
    }
  }

  getFormat(format: DateFormat): string {
    switch(format) {
      case DateFormat.dateTimeStandard: 
        return this.dateTimeStandard;
      case DateFormat.dateTimeFull: 
        return this.dateTimeFull;
      case DateFormat.dateOnly: 
        return this.dateOnly;
    }
  }

  toDate(value: string | null | undefined, format: DateFormat): Date | null {
    if(!value) return null;

    let dateStr: dayjs.Dayjs;

    switch(format) {
      case DateFormat.dateTimeStandard: 
        dateStr = dayjs(value, this.dateTimeStandard);
        break;
      case DateFormat.dateTimeFull: 
        dateStr = dayjs(value, this.dateTimeFull);
        break;
      case DateFormat.dateOnly: 
        dateStr = dayjs(value, this.dateOnly);
        break;
    }

    if(dateStr?.isValid()) {
      return dateStr.toDate();
    }
    else return null;
  }
}


export enum DateFormat {
  dateTimeStandard,
  dateTimeFull,
  dateOnly,
}