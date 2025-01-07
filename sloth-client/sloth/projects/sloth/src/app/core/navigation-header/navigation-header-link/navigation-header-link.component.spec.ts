import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationHeaderLinkComponent } from './navigation-header-link.component';

describe('NavigationHeaderLinkComponent', () => {
  let component: NavigationHeaderLinkComponent;
  let fixture: ComponentFixture<NavigationHeaderLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavigationHeaderLinkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NavigationHeaderLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
