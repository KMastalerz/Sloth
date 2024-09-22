import { Component, computed, input, model} from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';

import { WebControl } from '@sloth-http';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule],
  template: '',
})
export class BaseControl {
  parent = input<any>();
  config = input.required<WebControl | undefined>()
  metaData = computed<any>(
    () => this.config()?.metaData ? JSON.parse(<string>this.config()!.metaData) : undefined
  );

  //sort those in alphabetical order
  action = computed<string>(() => this.config()?.action || '');
  controlID = computed<string>(() => this.config()?.controlID || '');
  errorCount = computed<number | null>(() => this.metaData().errorCount);
  falseIcon = computed<string>(() => this.metaData().onFalse);
  icon = computed<string>(() => this.metaData()?.icon);
  iconHide = computed<string>(() => this.metaData()?.iconHide);
  iconShow = computed<string>(() => this.metaData()?.iconShow);
  isDisabled = computed<boolean>(() => this.config()?.isDisabled || false);
  isHidden = computed<boolean>(() => this.config()?.isHidden || false);
  isReadOnly = computed<boolean>(() => this.config()?.isReadOnly || false);
  label = computed<string>(() => this.config()?.controlLabel || '');
  placeholder = computed<string>(() => this.config()?.controlPlaceholder || '');
  route = computed<string>(() => this.config()?.route || '');
  routePageID = computed<string>(() => this.config()?.routePageID || '');
  size = computed<string>(() => this.metaData().size);
  tooltip = computed<string>(() => this.config()?.controlTooltip || '');
  trueIcon = computed<string>(() => this.metaData().onTrue);
  warningCount = computed<number | null>(() => this.metaData().warningCount);
}
