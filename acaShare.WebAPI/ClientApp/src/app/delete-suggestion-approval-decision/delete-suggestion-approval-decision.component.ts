import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common'
import { DeleteSuggestionApprovalDecision } from './DeleteSuggestionApprovalDecision';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-delete-suggestion-approval-decision',
  templateUrl: './delete-suggestion-approval-decision.component.html',
  styleUrls: ['./delete-suggestion-approval-decision.component.css']
})
export class DeleteSuggestionApprovalDecisionComponent implements OnInit {
  private apiUrl = `api/v1/DeleteSuggestions`;
  decision: DeleteSuggestionApprovalDecision;

  constructor(
    private http: HttpClient,
    private location: Location,
    private route: ActivatedRoute,
    ) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.http
      .get<DeleteSuggestionApprovalDecision>(`${this.apiUrl}/${id}`)
      .subscribe(d => this.decision = d, error => console.log(error));
  }

  goToPrevPage() {
    this.location.back();
  }

  onApproveClick() {

  }
}
