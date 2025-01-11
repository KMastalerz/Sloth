import { Component, forwardRef } from '@angular/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatError, MatHint } from '@angular/material/form-field';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ControlComponent } from '../../control.component';
import { BaseSelectComponent } from '../base-select.component';

@Component({
  selector: 'sl-checkbox',
  imports: [ControlComponent, MatCheckboxModule, FormsModule, MatHint, MatError],
  templateUrl: './checkbox.component.html',
  styleUrl: './checkbox.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CheckboxComponent),
      multi: true
    }
  ],
})
export class CheckboxComponent extends BaseSelectComponent {}
