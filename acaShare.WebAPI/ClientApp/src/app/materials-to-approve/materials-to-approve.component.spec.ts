import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaterialsToApproveComponent } from './materials-to-approve.component';

describe('MaterialsToApproveComponent', () => {
  let component: MaterialsToApproveComponent;
  let fixture: ComponentFixture<MaterialsToApproveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaterialsToApproveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaterialsToApproveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
