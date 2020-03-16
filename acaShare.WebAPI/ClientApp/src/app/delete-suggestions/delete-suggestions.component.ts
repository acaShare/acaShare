import { Component, OnInit } from '@angular/core';
import { DeleteSuggestion } from './deletesuggestion';
import { DeleteSuggestionsService } from './delete-suggestions.service';

@Component({
  selector: 'app-delete-suggestions',
  templateUrl: './delete-suggestions.component.html',
  styleUrls: ['./delete-suggestions.component.css']
})
export class DeleteSuggestionsComponent implements OnInit {
  deleteSuggestions: DeleteSuggestion[];
  iconClass = "material-icons list-icon change-request-type-icon tooltipped";

  constructor(private service: DeleteSuggestionsService) { }

  ngOnInit() {
    this.init();
  }

  init() {
    this.service
      .getDeleteSuggestions()
      .subscribe(s => this.deleteSuggestions = s, error => console.log(error));
  }

  onApproveDeleteSuggestion(id: number) {
    this.service.approveDeleteSuggestion(id);
    this.init();
  }
}
