import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormControlComponent } from '../../base/form-control/form-control.component';

@Component({
  selector: 'sl-password',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './password.component.html',
  styleUrl: './password.component.scss'
})
export class PasswordComponent extends FormControlComponent{
  constructor(){
    super();
    this.prompts.set(this.controlPrompts);
  }

  public prompts = signal<any>(undefined);
  protected show = signal<boolean>(false);
  buttonIcon = computed(()=> this.show() ? 'visibility_off' : 'visibility');
  currentType = computed(()=> this.show() ? 'text' : 'password')

  private controlPrompts: any = {
    required: 'Required',
  }
  
  protected toggleVisibility(): void {
    this.show.set(!this.show());
  }
}

