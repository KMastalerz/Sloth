import { Component, input } from '@angular/core';
import { FormArray, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { WebControl } from '@sloth-shared';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule],
  template: '',
})
export class BaseControl {
  pageForm = input<FormGroup | undefined>(undefined);
  panelForm = input<FormGroup | FormArray | undefined>(undefined);
  panelSectionForm = input<FormGroup | FormArray | undefined>(undefined);
  pageReference = input<any>(undefined);
  panelReference = input<any>(undefined);
  config = input.required<WebControl | undefined>()
  
  // metaData = computed<any>(
  //   () => this.config()?.metaData ? JSON.parse(<string>this.config()!.metaData) : undefined
  // );

  // //sort those in alphabetical order
  // action = computed<string>(() => this.config()?.action || '');
  // controlID = computed<string>(() => this.config()?.controlID || '');
  // errorCount = computed<number | null>(() => this.metaData().errorCount);
  // falseIcon = computed<string>(() => this.metaData().onFalse);
  // icon = computed<string>(() => this.metaData()?.icon);
  // iconHide = computed<string>(() => this.metaData()?.iconHide);
  // iconShow = computed<string>(() => this.metaData()?.iconShow);
  // isDisabled = computed<boolean>(() => this.config()?.isDisabled || false);
  // isHidden = computed<boolean>(() => this.config()?.isHidden || false);
  // isReadOnly = computed<boolean>(() => this.config()?.isReadOnly || false);
  // label = computed<string>(() => this.config()?.controlLabel || '');
  // onFalse = computed<string>(() => this.metaData().onFalse || '');
  // onTrue = computed<string>(() => this.metaData().onTrue || '');
  // placeholder = computed<string>(() => this.config()?.controlPlaceholder || '');
  // route = computed<string>(() => this.config()?.route || '');
  // routePageID = computed<string>(() => this.config()?.routePageID || '');
  // size = computed<string>(() => this.metaData().size);
  // tooltip = computed<string>(() => this.config()?.controlTooltip || '');
  // trueIcon = computed<string>(() => this.metaData().onTrue);
  // warningCount = computed<number | null>(() => this.metaData().warningCount);
}
