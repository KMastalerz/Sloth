import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ListUtilityService {
  IsEmpty(list: any[] | undefined): boolean {
    return list === undefined || list.length === 0;
  }
}
