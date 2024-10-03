import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicRouterFormComponent } from './dynamic-router-form.component';

describe('DynamicRouterFormComponent', () => {
  let component: DynamicRouterFormComponent;
  let fixture: ComponentFixture<DynamicRouterFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DynamicRouterFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DynamicRouterFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
