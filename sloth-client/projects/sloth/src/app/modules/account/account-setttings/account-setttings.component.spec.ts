import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountSetttingsComponent } from './account-setttings.component';

describe('AccountSetttingsComponent', () => {
  let component: AccountSetttingsComponent;
  let fixture: ComponentFixture<AccountSetttingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccountSetttingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountSetttingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
