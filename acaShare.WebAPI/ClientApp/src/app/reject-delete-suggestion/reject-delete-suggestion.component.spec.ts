import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RejectDeleteSuggestionComponent } from './reject-delete-suggestion.component';

describe('RejectDeleteSuggestionComponent', () => {
  let component: RejectDeleteSuggestionComponent;
  let fixture: ComponentFixture<RejectDeleteSuggestionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RejectDeleteSuggestionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RejectDeleteSuggestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
