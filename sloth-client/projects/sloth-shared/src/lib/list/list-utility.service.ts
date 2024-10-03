import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ListUtilityService {
  IsEmpty(list: any[] | undefined): boolean {
    return list === undefined || list.length === 0;
  }

  Contains(list: string[] | undefined, value: string | undefined): boolean {
    if (list === undefined || value === undefined) {
      return false;
    }
    return list.includes(value.toLowerCase());
  }
}
