import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavPanelComponent } from './side-nav-panel.component';

describe('SideNavPanelComponent', () => {
  let component: SideNavPanelComponent;
  let fixture: ComponentFixture<SideNavPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SideNavPanelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideNavPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
