import { Component, computed } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BasePage, ButtonComponent, HorizontalPanelComponent, LinkComponent } from '@sloth-ui';

@Component({
  selector: 'sl-product',
  standalone: true,
  imports: [RouterOutlet, HorizontalPanelComponent, LinkComponent, ButtonComponent],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent extends BasePage {
  link = computed(()=>this.pageSync()?.getControlByID('productList'));
  button = computed(()=>this.pageSync()?.getControlByID('addProduct'));
}
