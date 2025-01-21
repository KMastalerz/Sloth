import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseFormArrayControlComponent } from './base-form-array-control.component';

describe('BaseFormArrayControlComponent', () => {
  let component: BaseFormArrayControlComponent;
  let fixture: ComponentFixture<BaseFormArrayControlComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseFormArrayControlComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseFormArrayControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
