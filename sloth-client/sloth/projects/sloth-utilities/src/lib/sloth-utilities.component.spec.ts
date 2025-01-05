import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlothUtilitiesComponent } from './sloth-utilities.component';

describe('SlothUtilitiesComponent', () => {
  let component: SlothUtilitiesComponent;
  let fixture: ComponentFixture<SlothUtilitiesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SlothUtilitiesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlothUtilitiesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
