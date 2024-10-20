import { Injectable } from '@angular/core';
import { SlothIcon } from '../icons/sloth.icon';

@Injectable({
  providedIn: 'root'
})
export class IconService {
  private svgMap : Map<string, string> = new Map<string, string>();
  constructor(){
    this.registerIcons();
  }

  private registerIcons() {
    this.svgMap.set('sloth', SlothIcon);
  }

  getIcon(name: string): string | null {
    return this.svgMap.get(name) || null; 
  }
}
