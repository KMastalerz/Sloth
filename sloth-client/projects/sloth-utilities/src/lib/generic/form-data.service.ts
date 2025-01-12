import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FormDataService {
  // Generic method to convert any DTO to FormData
  toFormData(data: any, formData: FormData = new FormData(), parentKey?: string): FormData {

    if (data === null || data === undefined) {
      // You might skip appending these or handle them as needed
      return formData;
    }
  
    if (data instanceof Date) {
      // Convert Date to ISO-8601 string
      formData.append(parentKey ?? 'unknown', data.toISOString());
    }

    // If data is a File or Blob, append directly
    if (data instanceof File || data instanceof Blob) {
      formData.append(parentKey ?? 'file', data);
      return formData;
    }
  
    // If data is an array, iterate and append each item
    if (Array.isArray(data)) {
      data.forEach((element, index) => {
        // Use bracket notation for array items: parentKey[index]
        const arrayKey = parentKey ? `${parentKey}[${index}]` : String(index);
        this.toFormData(element, formData, arrayKey);
      });
      return formData;
    }
  
    // If data is a plain object, iterate over its keys
    if (typeof data === 'object') {
      Object.keys(data).forEach((key) => {
        const value = data[key];
        // Construct a nested key, e.g. parentKey[childKey]
        const newKey = parentKey ? `${parentKey}[${key}]` : key;
        this.toFormData(value, formData, newKey);
      });
      return formData;
    }
  
    // If data is a simple value (string, number, boolean), append directly
    formData.append(parentKey ?? 'unknown', String(data));
    return formData;
  }

}
