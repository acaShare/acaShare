import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeleteSuggestion } from './deletesuggestion';

@Component({
  selector: 'app-delete-suggestions',
  templateUrl: './delete-suggestions.component.html',
  styleUrls: ['./delete-suggestions.component.css']
})
export class DeleteSuggestionsComponent implements OnInit {
  private apiUrl = "api/v1/deletesuggestions";
  deleteSuggestions: DeleteSuggestion[];
  iconClass = "material-icons list-icon change-request-type-icon tooltipped";

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http
      .get<DeleteSuggestion[]>(this.apiUrl)
      .subscribe(s => this.deleteSuggestions = s, error => console.log(error));
  }

  approveDeleteSuggestion() {

  }

  rejectDeleteSuggestion() {

  }
}
