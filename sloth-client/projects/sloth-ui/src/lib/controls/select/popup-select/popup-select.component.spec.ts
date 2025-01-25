import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PopupSelectComponent } from './popup-select.component';

describe('PopupSelectComponent', () => {
  let component: PopupSelectComponent;
  let fixture: ComponentFixture<PopupSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PopupSelectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PopupSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
