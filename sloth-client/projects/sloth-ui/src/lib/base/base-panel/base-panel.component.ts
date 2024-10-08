import { Component, computed, model } from '@angular/core';
import { DynamicPageSync } from '../../page-sync/dynamic-page-sync';
import { WebControl, WebPanel } from '@sloth-http';

@Component({
  selector: 'sl-base-panel',
  standalone: true,
  template:''
})
export class BasePanel {
  pageSync = model.required<DynamicPageSync>();
  config = model.required<WebPanel>();
  metaData = computed<any>(() => JSON.parse(this.config()?.metaData ?? '{}'));
  sections = computed<WebControl[] | undefined>(() => {
    const orderedSections = this.config()?.sections?.split(',');
    const orderedControls = this.config()?.controls?.split(',');
    const orderedSectionsControls = orderedControls.map(control => this.config()?.webControls?.find((c: WebControl) => control === c.controlID));
    if(orderedSections && orderedSectionsControls) {
      return orderedSections.map(section => (orderedSectionsControls as WebControl[]).find((control: WebControl) => section === control.sectionID)) as WebControl[];
    }
    return undefined;
  });
}
