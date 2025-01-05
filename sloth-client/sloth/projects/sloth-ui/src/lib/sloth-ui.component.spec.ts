import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlothUiComponent } from './sloth-ui.component';

describe('SlothUiComponent', () => {
  let component: SlothUiComponent;
  let fixture: ComponentFixture<SlothUiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SlothUiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlothUiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
