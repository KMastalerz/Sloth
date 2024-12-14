import { Component, computed, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { NgStyle } from '@angular/common';
import { StyleGeneratorService } from '../../services/style-generator/style-generator.service';
import { DynamicPanelDirective } from '../../directives/dynamic-panel/dynamic-panel.directive';
import { BaseForm } from '../../base/base-form/base-form.component';
import { GeneratedGrid } from '../../models/forms.types';

@Component({
  selector: 'sl-dynamic-form',
  standalone: true,
  imports: [RouterOutlet, DynamicPanelDirective, NgStyle, ReactiveFormsModule],
  templateUrl: './dynamic-form.component.html',
  styleUrl: './dynamic-form.component.scss'
})
export class DynamicFormComponent extends BaseForm {
  styleGenerator = inject(StyleGeneratorService)
  generatedGrid = computed<GeneratedGrid>(() => {
    const style = this.styleGenerator.generateGrid(this.layout())
    console.log('gridStyle', style);
    
    return style;
  })
}
