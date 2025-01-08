import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegularLinkComponent } from './regular-link.component';

describe('RegularLinkComponent', () => {
  let component: RegularLinkComponent;
  let fixture: ComponentFixture<RegularLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegularLinkComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegularLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
