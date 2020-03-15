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

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http
      .get<DeleteSuggestion[]>(this.apiUrl)
      .subscribe(s => this.deleteSuggestions = s, error => console.log(error));
  }

  // getIcon(reasonId: number) {
  //   switch (reasonId)
  //   {
  //       case 1:
  //           return "warning";
  //       case 2:
  //           return "pan_tool";
  //       case 3:
  //         return "receipt";
  //       case 4:
  //         return "location_off";
  //       case 5:
  //         return "feedback";
  //   }
  // }

  approveDeleteSuggestion() {

  }

  rejectDeleteSuggestion() {

  }
}
