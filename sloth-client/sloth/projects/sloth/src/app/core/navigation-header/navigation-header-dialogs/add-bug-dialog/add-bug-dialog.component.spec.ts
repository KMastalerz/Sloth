import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBugDialogComponent } from './add-bug-dialog.component';

describe('AddBugDialogComponent', () => {
  let component: AddBugDialogComponent;
  let fixture: ComponentFixture<AddBugDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBugDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBugDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
