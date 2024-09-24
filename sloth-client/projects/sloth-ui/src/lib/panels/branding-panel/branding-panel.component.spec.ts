import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandingPanelComponent } from './branding-panel.component';

describe('BrandingPanelComponent', () => {
  let component: BrandingPanelComponent;
  let fixture: ComponentFixture<BrandingPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BrandingPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrandingPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
