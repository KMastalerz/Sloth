import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarkdownInputComponent } from './markdown-input.component';

describe('MarkdownInputComponent', () => {
  let component: MarkdownInputComponent;
  let fixture: ComponentFixture<MarkdownInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MarkdownInputComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarkdownInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
