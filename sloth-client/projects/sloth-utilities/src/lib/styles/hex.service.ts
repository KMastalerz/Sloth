import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HexService {
  getAccessibleFontColor(hexColor: string | null | undefined): 'white' | 'black' | null{
    if(!hexColor) return null;

    // Remove the '#' if present
    const sanitizedHex = hexColor.replace('#', '');

    // Parse the hex color into RGB components
    const r = parseInt(sanitizedHex.substring(0, 2), 16);
    const g = parseInt(sanitizedHex.substring(2, 4), 16);
    const b = parseInt(sanitizedHex.substring(4, 6), 16);

    // Calculate the relative luminance (per WCAG standards)
    const luminance = 0.2126 * this.relativeLuminance(r) +
                      0.7152 * this.relativeLuminance(g) +
                      0.0722 * this.relativeLuminance(b);

    // Return "dark" if luminance is greater than 0.5, else "light"
    return luminance > 0.5 ? 'black' : 'white';
  }

  private relativeLuminance(channel: number): number {
    const normalized = channel / 255;
    return normalized <= 0.03928
      ? normalized / 12.92
      : Math.pow((normalized + 0.055) / 1.055, 2.4);
  }
}
