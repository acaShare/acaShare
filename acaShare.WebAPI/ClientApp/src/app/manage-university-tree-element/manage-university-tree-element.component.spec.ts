import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageUniversityTreeElementComponent } from './manage-university-tree-element.component';

describe('ManageUniversityTreeElementComponent', () => {
  let component: ManageUniversityTreeElementComponent;
  let fixture: ComponentFixture<ManageUniversityTreeElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageUniversityTreeElementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageUniversityTreeElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
