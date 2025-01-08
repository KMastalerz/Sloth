import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlothDtoComponent } from './sloth-dto.component';

describe('SlothDtoComponent', () => {
  let component: SlothDtoComponent;
  let fixture: ComponentFixture<SlothDtoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SlothDtoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlothDtoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
