import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlothHttpComponent } from './sloth-http.component';

describe('SlothHttpComponent', () => {
  let component: SlothHttpComponent;
  let fixture: ComponentFixture<SlothHttpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SlothHttpComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlothHttpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
