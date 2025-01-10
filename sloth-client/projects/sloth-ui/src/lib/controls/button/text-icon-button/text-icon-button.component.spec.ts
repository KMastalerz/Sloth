import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TextIconButtonComponent } from './text-icon-button.component';

describe('TextIconButtonComponent', () => {
  let component: TextIconButtonComponent;
  let fixture: ComponentFixture<TextIconButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TextIconButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TextIconButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
