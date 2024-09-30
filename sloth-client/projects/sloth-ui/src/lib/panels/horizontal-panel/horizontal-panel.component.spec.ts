import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HorizontalPanelComponent } from './horizontal-panel.component';

describe('HorizontalPanelComponent', () => {
  let component: HorizontalPanelComponent;
  let fixture: ComponentFixture<HorizontalPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HorizontalPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HorizontalPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
