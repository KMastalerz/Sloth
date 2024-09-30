import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerticalPanelComponent } from './vertical-panel.component';

describe('VerticalPanelComponent', () => {
  let component: VerticalPanelComponent;
  let fixture: ComponentFixture<VerticalPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VerticalPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VerticalPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
