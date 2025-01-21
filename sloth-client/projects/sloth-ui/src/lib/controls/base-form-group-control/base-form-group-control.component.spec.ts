import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseFormGroupControlComponent } from './base-form-group-control.component';

describe('BaseFormGroupControlComponent', () => {
  let component: BaseFormGroupControlComponent;
  let fixture: ComponentFixture<BaseFormGroupControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseFormGroupControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseFormGroupControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
