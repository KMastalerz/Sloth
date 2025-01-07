import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddReleaseDialogComponent } from './add-release-dialog.component';

describe('AddReleaseDialogComponent', () => {
  let component: AddReleaseDialogComponent;
  let fixture: ComponentFixture<AddReleaseDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddReleaseDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddReleaseDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
