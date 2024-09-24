import { Injectable } from '@angular/core';
import dayjs from 'dayjs';

@Injectable({
  providedIn: 'root'
})
export class DateUtilityService {
  toDateTime(date: string): Date {
    return new Date(date);
  }

  isBefore(endDate: string | number | Date, startDate?: string | number | Date | null): boolean {
    return dayjs(startDate).isBefore(endDate);
  }
}

