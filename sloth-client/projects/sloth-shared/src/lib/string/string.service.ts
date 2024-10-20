import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StringService {
  // Convert a string to camel case format (e.g. 'hello-world' to 'helloWorld')
  toCamelCase(input?: string): string | undefined {
    return input?.toLowerCase()
                .split('-')
                .map((word, index) => index === 0 ? word : word.charAt(0).toUpperCase() + word.slice(1))
                .join('');
  }
}
