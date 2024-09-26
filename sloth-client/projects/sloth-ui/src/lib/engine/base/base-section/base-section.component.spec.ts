import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseSection } from './base-section.component';

describe('BaseSection', () => {
  let component: BaseSection;
  let fixture: ComponentFixture<BaseSection>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BaseSection]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BaseSection);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
