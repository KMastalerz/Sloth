import { Component, computed, input} from '@angular/core';

import { WebControl } from '@sloth-http';

@Component({
  template: '',
})
export class BaseControl {
  config = input.required<WebControl>()
  metaData = computed<any>(
    () => this.config().MetaData ? JSON.parse(<string>this.config().MetaData) : undefined
  );
}
