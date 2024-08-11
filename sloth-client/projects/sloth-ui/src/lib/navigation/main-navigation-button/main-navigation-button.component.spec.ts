import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainNavigationButtonComponent } from './main-navigation-button.component';

describe('MainNavigationButtonComponent', () => {
  let component: MainNavigationButtonComponent;
  let fixture: ComponentFixture<MainNavigationButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MainNavigationButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MainNavigationButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
