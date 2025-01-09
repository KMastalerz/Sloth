import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CacheService {

  private encryptionKey: string = 'MySecretKey'; // Change this to a secure key

  constructor() { }

  // Encrypt and serialize data before saving it
  private encryptData<T>(data: T): string {
    const jsonData = JSON.stringify(data);
    const encrypted = CryptoJS.AES.encrypt(jsonData, this.encryptionKey).toString();
    return encrypted;
  }

  // Decrypt and parse data when reading it
  private decryptData<T>(encryptedData: string): T | null {
    try {
      const bytes = CryptoJS.AES.decrypt(encryptedData, this.encryptionKey);
      const decryptedData = bytes.toString(CryptoJS.enc.Utf8);
      return decryptedData ? JSON.parse(decryptedData) as T : null;
    } catch (e) {
      console.error('Decryption failed', e);
      return null;
    }
  }

  // Save to LocalStorage
  saveToLocalStorage<T>(key: string, value: T): void {
    const encryptedValue = this.encryptData(value);
    localStorage.setItem(key, encryptedValue);
  }

  // Read from LocalStorage
  readFromLocalStorage<T>(key: string): T | null {
    const encryptedValue = localStorage.getItem(key);
    return encryptedValue ? this.decryptData<T>(encryptedValue) : null;
  }

  // Save to SessionStorage
  saveToSessionStorage<T>(key: string, value: T): void {
    const encryptedValue = this.encryptData(value);
    sessionStorage.setItem(key, encryptedValue);
  }

  // Read from SessionStorage
  readFromSessionStorage<T>(key: string): T | null {
    const encryptedValue = sessionStorage.getItem(key);
    return encryptedValue ? this.decryptData<T>(encryptedValue) : null;
  }

  // Save to Cookies
  saveToCookies<T>(key: string, value: T, expiresDays: number = 7): void {
    const encryptedValue = this.encryptData(value);
    const expirationDate = new Date();
    expirationDate.setTime(expirationDate.getTime() + (expiresDays * 24 * 60 * 60 * 1000)); // set expiration date
    const cookieValue = `${key}=${encryptedValue};expires=${expirationDate.toUTCString()};path=/`;
    document.cookie = cookieValue;
  }

  // Read from Cookies
  readFromCookies<T>(key: string): T | null {
    const name = `${key}=`;
    const decodedCookie = decodeURIComponent(document.cookie);
    const ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) === 0) {
        const encryptedValue = c.substring(name.length, c.length);
        return this.decryptData<T>(encryptedValue);
      }
    }
    return null;
  }

  // Delete from Cookies
  deleteCookie(key: string): void {
    document.cookie = `${key}=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/`;
  }
}
