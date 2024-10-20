import { Component, computed, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgStyle } from '@angular/common';
import { StyleGeneratorService } from '../../services/style-generator/style-generator.service';
import { DynamicPanelDirective } from '../../directives/dynamic-panel/dynamic-panel.directive';
import { BaseForm } from '../../base/base-form/base-form.component';
import { GeneratedGrid } from '../../models/forms.types';

@Component({
  selector: 'sl-dynamic-grid',
  standalone: true,
  imports: [RouterOutlet, DynamicPanelDirective, NgStyle],
  templateUrl: './dynamic-grid.component.html',
  styleUrl: './dynamic-grid.component.scss'
})
export class DynamicGridComponent extends BaseForm {
  styleGenerator = inject(StyleGeneratorService)
  generatedGrid = computed<GeneratedGrid>(() => {
    const style = this.styleGenerator.generateGrid(this.metaData())
    console.log('gridStyle', style);
    
    return style;
  })
}
