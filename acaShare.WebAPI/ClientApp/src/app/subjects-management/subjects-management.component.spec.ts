import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubjectsManagementComponent } from './subjects-management.component';

describe('SubjectsManagementComponent', () => {
  let component: SubjectsManagementComponent;
  let fixture: ComponentFixture<SubjectsManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubjectsManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubjectsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
