import { inject, Injectable } from '@angular/core';
import { CacheService } from '../cache/cache.service';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private cacheService = inject(CacheService);

  
}
