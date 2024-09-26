import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleSectionComponent } from './simple-section.component';

describe('SimpleSectionComponent', () => {
  let component: SimpleSectionComponent;
  let fixture: ComponentFixture<SimpleSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SimpleSectionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SimpleSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
