import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeleteSuggestion } from './deletesuggestion';

@Injectable({
  providedIn: 'root'
})
export class DeleteSuggestionsService {
  private apiUrl = "api/v1/DeleteSuggestions";

  constructor(private http: HttpClient) { }

  getDeleteSuggestions() {
    return this.http.get<DeleteSuggestion[]>(this.apiUrl);
  }

  approveDeleteSuggestion(id: number) {
    const deleteRequestId: number = id;
    const declineReason: string = '';
    const shouldApprove: boolean = true;
    this.changeSuggestionState(deleteRequestId, declineReason, shouldApprove);
  }

  rejectDeleteSuggestion(id: number, reason: string) {
    const deleteRequestId: number = id;
    const declineReason: string = reason;
    const shouldApprove: boolean = false;
    this.changeSuggestionState(deleteRequestId, declineReason, shouldApprove);
  }

  changeSuggestionState(deleteRequestId: number, declineReason: string, shouldApprove: boolean) {
    const dto = { deleteRequestId, declineReason, shouldApprove };
    this.http.put(this.apiUrl, dto).subscribe(error => console.log(error));
  }
}
