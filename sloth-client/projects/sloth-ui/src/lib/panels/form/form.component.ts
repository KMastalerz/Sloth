import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BasePanel } from '../../base/base-panel/base-panel.component';
import { BrandingSectionComponent } from '../../sections/branding-section/branding-section.component';
import { DynamicControlDirective } from '../../directives/dynamic-control/dynamic-control.directive';

@Component({
  selector: 'sl-form',
  standalone: true,
  imports: [ReactiveFormsModule, BrandingSectionComponent, DynamicControlDirective],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent extends BasePanel {}
