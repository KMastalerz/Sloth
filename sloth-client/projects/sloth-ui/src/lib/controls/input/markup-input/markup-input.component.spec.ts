import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarkupInputComponent } from './markup-input.component';

describe('MarkupInputComponent', () => {
  let component: MarkupInputComponent;
  let fixture: ComponentFixture<MarkupInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MarkupInputComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarkupInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
