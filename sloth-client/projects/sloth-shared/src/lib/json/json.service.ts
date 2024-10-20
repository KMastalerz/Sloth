import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JsonService {

    tryParse(json: string | undefined | null): any {
        try {
            if(!json) return json;
            return JSON.parse(json);
        } catch (e) {
            console.error('Error while parsing JSON (error)', e);
            console.error('Error while parsing JSON (value)', json);
            return null;
        }
    }
}
