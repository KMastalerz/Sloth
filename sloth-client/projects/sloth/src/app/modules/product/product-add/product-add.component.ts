import { Component, computed } from '@angular/core';
import { BasePage, InputComponent, VerticalPanelComponent } from '@sloth-ui';

@Component({
  selector: 'sl-product-add',
  standalone: true,
  imports: [VerticalPanelComponent, InputComponent],
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.scss'
})
export class ProductAddComponent extends BasePage {
  formControls = computed(()=> this.pageSync().getControlBySectionID('details'));
}
