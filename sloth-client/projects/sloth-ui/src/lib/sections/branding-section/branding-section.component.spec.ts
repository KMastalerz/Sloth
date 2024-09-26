import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandingSectionComponent } from './branding-section.component';

describe('BrandingSectionComponent', () => {
  let component: BrandingSectionComponent;
  let fixture: ComponentFixture<BrandingSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrandingSectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrandingSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
