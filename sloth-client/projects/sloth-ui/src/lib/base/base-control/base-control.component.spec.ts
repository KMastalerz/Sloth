import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseControl } from './base-control.component';

describe('BaseControl', () => {
  let component: BaseControl;
  let fixture: ComponentFixture<BaseControl>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseControl]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseControl);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
