import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EventBlockerDirective } from 'sloth-utilities';


describe('EventBlockerDirective', () => {
  let component: EventBlockerDirective;
  let fixture: ComponentFixture<EventBlockerDirective>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventBlockerDirective]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventBlockerDirective);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
