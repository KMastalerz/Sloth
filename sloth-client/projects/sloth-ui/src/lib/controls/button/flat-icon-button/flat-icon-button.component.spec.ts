import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlatIconButtonComponent } from './flat-icon-button.component';

describe('FlatIconButtonComponent', () => {
  let component: FlatIconButtonComponent;
  let fixture: ComponentFixture<FlatIconButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FlatIconButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlatIconButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
