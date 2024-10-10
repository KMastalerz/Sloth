import { Injectable } from '@angular/core';
import { HSL, RGB, Theme } from '../../models/theme.model';
import { Colors, Factor, Shades, ThemeType } from '../../constants/theme.constants';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  constructor(){
    const themes: Theme[] = [];
    Colors.Colors.forEach(theme => {
      let rgb: RGB = this.hexToRgb(theme.hex.toString()) ?? { red: 0, green: 0, blue: 0 };
      switch(theme.type){
        case ThemeType.Regular:
          const regularFactors = Shades.Regular;
          this.generateTintOrShade(rgb, Factor.Regular, theme.name, regularFactors, themes); 
          break;
        case ThemeType.Information:
          const infoFactors = Shades.Information;
          this.generateTintOrShade(rgb, Factor.Regular, theme.name, infoFactors, themes); 
          break;
        case ThemeType.Neutral:
          const neutralFactors = Shades.Neutral;
          this.generateTintOrShade(rgb, Factor.Neutral, theme.name, neutralFactors, themes); 
          break
      }
    });

    // Populate the css variables
    themes.forEach(theme => {
      document.documentElement.style.setProperty(`${theme.key}`, theme.value);
    });
  }

  private generateTintOrShade(rgb: RGB, factor: number, name: string, shades: number[], themes: Theme[]): void {
    shades.forEach(shade => { 
      const tintShade = shade < 0 ? 'shade' : 'tint';
      const generatedTintShade = shade < 0  ? this.generateShade(rgb, factor, shade) : this.generateTint(rgb, factor, shade);

      const themeKey = `--sl-${name}-${tintShade}-${Math.abs(shade)}`;
      const themeValue = this.rgbToHex(generatedTintShade);
      const theme = { key: themeKey, value: themeValue };

      const onThemeKey = `--sl-on-${name}-${tintShade}-${Math.abs(shade)}`;
      const onThemeValue = this.onTintShadeColor(generatedTintShade);
      const onTheme = { key: onThemeKey, value: onThemeValue };
      
      themes.push(theme);
      themes.push(onTheme);
    });
  }

  private onTintShadeColor(rgb: RGB): string {
    return (rgb.red * 0.299 + rgb.green * 0.587 + rgb.blue * 0.114) > 186 ? '#0A0A0A' : '#F5F5F5';
  }

  private generateTint(rgb: RGB, factor: number, shade: number): RGB {
    rgb.red = Math.round(rgb.red + (255 - rgb.red) * factor * shade / 100);
    rgb.green = Math.round(rgb.green + (255 - rgb.green) * factor * shade / 100);
    rgb.blue = Math.round(rgb.blue + (255 - rgb.blue) * factor * shade / 100);

    rgb.red = rgb.red > 255 ? 255 : rgb.red;
    rgb.green = rgb.green > 255 ? 255 : rgb.green;
    rgb.blue = rgb.blue > 255 ? 255 : rgb.blue;

    return rgb;
  }

  private generateShade(rgb: RGB, factor: number, shade: number): RGB {
    
    rgb.red = Math.round(rgb.red - (rgb.red * factor * shade / 100));
    rgb.green = Math.round(rgb.green - (rgb.green * factor * shade / 100));
    rgb.blue = Math.round(rgb.blue - (rgb.blue * factor * shade / 100));

    return rgb;
  }

  private hexToRgb(hex: string): RGB | null { 
    const validHex = /^#?([a-fA-F\d]{2})([a-fA-F\d]{2})([a-fA-F\d]{2})$/;
    const result = validHex.exec(hex);
    if (!result) {
      console.log('Invalid hex color');
      return null;
    }

    const red = parseInt(result[1], 16);
    const green = parseInt(result[2], 16);
    const blue = parseInt(result[3], 16);

    return { red, green, blue };
  }

  private hexToHsl(hex: string): HSL | null {
    const rgb = this.hexToRgb(hex);
    if (!rgb) return null;

    return this.rgbToHsl(rgb);
  }

  private rgbToHex({ red, green, blue }: RGB): string {
    const r = red.toString(16).padStart(2, '0');
    const g = green.toString(16).padStart(2, '0');
    const b = blue.toString(16).padStart(2, '0');

    return `#${r}${g}${b}`;
  }

  private rgbToHsl({ red, green, blue }: RGB): HSL {
    const r = red / 255;
    const g = green / 255;
    const b = blue / 255;

    const max = Math.max(r, g, b);
    const min = Math.min(r, g, b);
    let hue = 0;
    let saturation = 0;
    const light = (max + min) / 2;

    if (max !== min) {
        const delta = max - min;
        saturation = light > 0.5 ? delta / (2 - max - min) : delta / (max + min);
        
        switch (max) {
            case r: 
                hue = (g - b) / delta + (g < b ? 6 : 0);
                break;
            case g: 
                hue = (b - r) / delta + 2;
                break;
            case b: 
                hue = (r - g) / delta + 4;
                break;
        }
        hue /= 6;
    }

    return {
        hue: hue * 360,
        saturation: saturation * 100,
        light: light * 100,
    };
  }

  private hslToRgb({ hue, saturation, light }: HSL): RGB {
    const h = hue / 360;
    const s = saturation / 100;
    const l = light / 100;

    const hueToRgb = (p: number, q: number, t: number) => {
        if (t < 0) t += 1;
        if (t > 1) t -= 1;
        if (t < 1 / 6) return p + (q - p) * 6 * t;
        if (t < 1 / 3) return q;
        if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
        return p;
    };

    let red: number, green: number, blue: number;

    if (s === 0) {
        red = green = blue = l * 255;
    } else {
        const q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        const p = 2 * l - q;
        red = hueToRgb(p, q, h + 1 / 3) * 255;
        green = hueToRgb(p, q, h) * 255;
        blue = hueToRgb(p, q, h - 1 / 3) * 255;
    }

    return {
        red: Math.round(red),
        green: Math.round(green),
        blue: Math.round(blue),
    };
  }

  private hslToHex(hsl: HSL): string {
    const rgb = this.hslToRgb(hsl);
    return this.rgbToHex(rgb);
  }
}
