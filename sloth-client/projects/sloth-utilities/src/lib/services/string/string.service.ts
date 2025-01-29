import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StringService {

  toInitialLowerCase(value: string): string {
    if (value.length === 0) {
      return value.toString();
    }
    return value.charAt(0).toLowerCase() + value.slice(1);
  };
}
