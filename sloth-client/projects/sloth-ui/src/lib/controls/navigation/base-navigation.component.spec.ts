import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseNavigationComponent } from './base-navigation.component';

describe('BaseNavigationComponent', () => {
  let component: BaseNavigationComponent;
  let fixture: ComponentFixture<BaseNavigationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseNavigationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
