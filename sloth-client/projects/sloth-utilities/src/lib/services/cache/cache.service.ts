import { inject, Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  readonly cookieService = inject(CookieService);
  private readonly masterKey: string = 'SuperSecureMasterKey'; // A static, secure master key

  constructor() {}

  // Helper to create a consistent key hash
  private hashKey(key: string): string {
    return CryptoJS.SHA256(key).toString(CryptoJS.enc.Hex); // Create a deterministic hash for keys
  }

  // Encrypt and serialize data with a random IV
  private encryptData<T>(data: T): string {
    const jsonData = JSON.stringify(data);
    const base64Encoded = btoa(jsonData);
    const encrypted = CryptoJS.AES.encrypt(base64Encoded, this.masterKey).toString(); // Random IV for strong security
    return encrypted;
  }

  // Decrypt and deserialize data
  private decryptData<T>(encryptedData: string): T | null {
    try {
      const bytes = CryptoJS.AES.decrypt(encryptedData, this.masterKey);
      const base64Decoded = bytes.toString(CryptoJS.enc.Utf8);
      const jsonData = atob(base64Decoded);
      return JSON.parse(jsonData) as T;
    } catch (e) {
      return null;
    }
  }

  // Save to Cookies with deterministic key hash
  saveToCookies<T>(key: string, value: T, expiresDays: number = 7): void {
    const hashedKey = this.hashKey(key); // Consistent hashed key
    const encryptedValue = this.encryptData(value);
    const expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + (expiresDays * 24 * 60 * 60 * 1000));
    this.cookieService.set(hashedKey, encryptedValue, expirationDate, '/');
  }

  // Read from Cookies with deterministic key hash
  readFromCookies<T>(key: string): T | null {
    const hashedKey = this.hashKey(key);
    const encryptedValue = this.cookieService.get(hashedKey);
    return encryptedValue ? this.decryptData<T>(encryptedValue) : null;
  }

  // Remove from Cookies with deterministic key hash
  removeFromCookies(key: string): void {
    const hashedKey = this.hashKey(key);
    this.cookieService.delete(hashedKey, '/');
  }

  // Save to LocalStorage with deterministic key hash
  saveToLocalStorage<T>(key: string, value: T): void {
    const hashedKey = this.hashKey(key);
    const encryptedValue = this.encryptData(value);
    localStorage.setItem(hashedKey, encryptedValue);
  }

  // Read from LocalStorage with deterministic key hash
  readFromLocalStorage<T>(key: string): T | null {
    const hashedKey = this.hashKey(key);
    const encryptedValue = localStorage.getItem(hashedKey);
    return encryptedValue ? this.decryptData<T>(encryptedValue) : null;
  }

  // Remove from LocalStorage with deterministic key hash
  removeFromLocalStorage(key: string): void {
    const hashedKey = this.hashKey(key);
    localStorage.removeItem(hashedKey);
  }

  // Save to SessionStorage with deterministic key hash
  saveToSessionStorage<T>(key: string, value: T): void {
    const hashedKey = this.hashKey(key);
    const encryptedValue = this.encryptData(value);
    sessionStorage.setItem(hashedKey, encryptedValue);
  }

  // Read from SessionStorage with deterministic key hash
  readFromSessionStorage<T>(key: string): T | null {
    const hashedKey = this.hashKey(key);
    const encryptedValue = sessionStorage.getItem(hashedKey);
    return encryptedValue ? this.decryptData<T>(encryptedValue) : null;
  }

  // Remove from SessionStorage with deterministic key hash
  removeFromSessionStorage(key: string): void {
    const hashedKey = this.hashKey(key);
    sessionStorage.removeItem(hashedKey);
  }
}