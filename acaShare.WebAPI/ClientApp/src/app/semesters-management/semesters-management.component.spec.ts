import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SemestersManagementComponent } from './semesters-management.component';

describe('SemestersManagementComponent', () => {
  let component: SemestersManagementComponent;
  let fixture: ComponentFixture<SemestersManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SemestersManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SemestersManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
