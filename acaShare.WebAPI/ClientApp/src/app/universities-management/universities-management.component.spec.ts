import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UniversitiesManagementComponent } from './universities-management.component';

describe('UniversitiesManagementComponent', () => {
  let component: UniversitiesManagementComponent;
  let fixture: ComponentFixture<UniversitiesManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UniversitiesManagementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UniversitiesManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
