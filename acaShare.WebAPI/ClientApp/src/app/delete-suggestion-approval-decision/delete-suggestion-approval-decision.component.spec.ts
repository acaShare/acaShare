import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteSuggestionApprovalDecisionComponent } from './delete-suggestion-approval-decision.component';

describe('DeleteSuggestionApprovalDecisionComponent', () => {
  let component: DeleteSuggestionApprovalDecisionComponent;
  let fixture: ComponentFixture<DeleteSuggestionApprovalDecisionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeleteSuggestionApprovalDecisionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeleteSuggestionApprovalDecisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
