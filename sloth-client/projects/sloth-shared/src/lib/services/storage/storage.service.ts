import { inject, Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { compressToBase64, decompressFromBase64  } from 'lz-string';
import { AES, enc } from 'crypto-js';
import { StorageType } from '../../constants/storage-type.constants';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private cookieService = inject(CookieService)
  private encryptionToken: string = '123sod123asdkj123';
  // Set encryption key after login
  setEncryptionToken(token: string) {
    this.encryptionToken = token;
  }
  
  // Helper to encrypt and compress data
  private encrypt(data: string): string {
    // Encrypt the data
    const encryptedData = AES.encrypt(data, this.encryptionToken).toString();
    // Compress the encrypted data
    return compressToBase64(encryptedData);
  }

  // Helper to decrypt and decompress data
  private decrypt(data: string): string {
    try {
      // Decompress the encrypted data
      const decompressedData = decompressFromBase64(data);
      if (!decompressedData) return '';
      // Decrypt the decompressed data
      const bytes = AES.decrypt(decompressedData, this.encryptionToken);
      return bytes.toString(enc.Utf8);
    } catch (e) {
      return '';
    }
  }

  // Set data
  setItem(key: string, value: string, storageType: StorageType): void {
    const compressedEncryptedValue = this.encrypt(value);

    switch (storageType) {
      case StorageType.COOKIE:
        this.cookieService.set(key, compressedEncryptedValue, undefined, undefined, undefined, true, 'Strict');
        break;
      case StorageType.SESSION:
        sessionStorage.setItem(key, compressedEncryptedValue);
        break;
      case StorageType.LOCAL:
        localStorage.setItem(key, compressedEncryptedValue);
        break;
      default:
        console.error('Invalid storage type');
    }
  }

  // Get data
  getItem(key: string, storageType: StorageType): string | null {
    let compressedEncryptedValue: string | null = null;

    switch (storageType) {
      case StorageType.COOKIE:
        compressedEncryptedValue = this.cookieService.get(key);
        break;
      case StorageType.SESSION:
        compressedEncryptedValue = sessionStorage.getItem(key);
        break;
      case StorageType.LOCAL:
        compressedEncryptedValue = localStorage.getItem(key);
        break;
      default:
        console.error('Invalid storage type');
    }

    return compressedEncryptedValue ? this.decrypt(compressedEncryptedValue) : null;
  }

  // Remove item
  removeItem(key: string, storageType: StorageType): void {
    switch (storageType) {
      case StorageType.COOKIE:
        this.cookieService.delete(key);
        break;
      case StorageType.SESSION:
        sessionStorage.removeItem(key);
        break;
      case StorageType.LOCAL:
        localStorage.removeItem(key);
        break;
      default:
        console.error('Invalid storage type');
    }
  }

  // Clear all data
  clear(storageType: StorageType): void {
    switch (storageType) {
      case StorageType.COOKIE:
        this.cookieService.deleteAll();
        break;
      case StorageType.SESSION:
        sessionStorage.clear();
        break;
      case StorageType.LOCAL:
        localStorage.clear();
        break;
      default:
        console.error('Invalid storage type');
    }
  }
}
