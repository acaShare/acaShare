import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { DeleteSuggestionsService } from '../delete-suggestions/delete-suggestions.service';
import { ActivatedRoute } from '@angular/router';
import { RejectDeleteSuggestionModel } from './rejectDeleteSuggestionModel';

@Component({
  selector: 'app-reject-delete-suggestion',
  templateUrl: './reject-delete-suggestion.component.html',
  styleUrls: ['./reject-delete-suggestion.component.css']
})
export class RejectDeleteSuggestionComponent implements OnInit {
  suggestion: RejectDeleteSuggestionModel = new RejectDeleteSuggestionModel();

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private service: DeleteSuggestionsService) { }

  ngOnInit() {
    
  }
  
  rejectDeleteSuggestion() {
    const id = parseInt(this.route.snapshot.paramMap.get('id'));
    this.service.rejectDeleteSuggestion(id, 'some reason');
    this.goToPrevPage();
  }

  goToPrevPage() {
    this.location.back();
  }
}
