import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddQueryDialogComponent } from './add-query-dialog.component';

describe('AddQueryDialogComponent', () => {
  let component: AddQueryDialogComponent;
  let fixture: ComponentFixture<AddQueryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddQueryDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddQueryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
