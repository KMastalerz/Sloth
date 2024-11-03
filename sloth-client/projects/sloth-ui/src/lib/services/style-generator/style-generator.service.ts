import { Injectable } from '@angular/core';
import { GeneratedGrid, GridStyle } from '../../models/forms.types';
import { PageLayoutMetadata } from '../../models/meta-data.types';

@Injectable({
  providedIn: 'root'
})
export class StyleGeneratorService {
  generateGrid(metadata: PageLayoutMetadata | null): GeneratedGrid {
    const convertToCssValue = (value: string | number): string => {
      if (value === 'auto') {
        return 'auto';
      } else if (typeof value === 'number') {
        return `${value}fr`;  // integer values are converted to `fr`
      } else {
        return value;  // values like '100px', '1rem', etc. are returned as is
      }
    };

    if(metadata === null) {
      const areas = ['sl-grid-area-1']
      const styles: GridStyle = {
        'grid-template-columns': '1',
        'grid-template-rows': '1',
        'grid-template-areas': '{sl-grid-area-1}'
      };
    
      return {styles, areas} as GeneratedGrid;
    }

    const areas: string[] = [];
    const gridTemplateColumns = metadata!.columnsRatio?.map(convertToCssValue).join(' ');
    const gridTemplateRows = metadata!.rowsRatio?.map(convertToCssValue).join(' ');
  
    // Initialize styles for grid-template-areas
    const gridAreaMatrix: string[][] = Array(metadata!.rows).fill(null).map(() => Array(metadata!.columns).fill('.'));
  
    if (metadata!.gridAreas) {
      metadata!.gridAreas.forEach(span => {
        const areaName = `sl-grid-area-${span.id}`;
        areas.push(areaName);
        // For each row span, place the area in the matrix
        if (span.type === 'row') {
          for (let row = span.spanFrom; row <= span.spanTo; row++) {
            gridAreaMatrix[row-1][span.id-1] = areaName;
          }
        }
        
        // For column spans, similar logic applies (if needed)
        if (span.type === 'column') {
          for (let col = span.spanFrom; col <= span.spanTo; col++) {
            gridAreaMatrix[span.id-1][col-1] = areaName;
          }
        }
      });
    }
  
    // Ensure that any remaining empty areas get unique names
    let areaCounter = metadata!.gridAreas?.length; 
    areaCounter = areaCounter ? areaCounter + 1 : 1;
    for (let row = 0; row < metadata!.rows!; row++) {
      for (let col = 0; col < metadata!.columns!; col++) {
        if (gridAreaMatrix[row][col] === '.') {
          const areaName = `sl-grid-area-${areaCounter++}`;
          areas.push(areaName);
          gridAreaMatrix[row][col] = areaName;
        }
      }
    }
  
    // Convert the matrix into grid-template-areas CSS
    const gridTemplateAreas = gridAreaMatrix.map(row => `'${row.join(' ')}'`).join(' ');
  
    const styles: GridStyle = {
      'grid-template-columns': gridTemplateColumns ?? '',
      'grid-template-rows': gridTemplateRows ?? '',
      'grid-template-areas': gridTemplateAreas
    };
  
    return {styles, areas} as GeneratedGrid;
  }
}
