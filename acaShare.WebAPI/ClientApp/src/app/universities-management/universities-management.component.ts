import { Component, OnInit, OnDestroy } from '@angular/core';
import { University } from './university';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-universities-management',
  templateUrl: './universities-management.component.html',
  styleUrls: ['./universities-management.component.css']
})
export class UniversitiesManagementComponent implements OnInit, OnDestroy {
  apiUrl = 'api/v1/universities';
  universities: University[];

  private activeSubscriptions: Subscription[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.activeSubscriptions.push(
      this.http
        .get<University[]>(this.apiUrl)
        .subscribe(u => this.universities = u, error => console.log(error))
    );
  }

  onApproveDeleteUniversity(id: number): void {
    this.activeSubscriptions.push(
      this.http
        .delete(`${this.apiUrl}/${id}`)
        .subscribe({
          complete: () => {
            const universityToDelete = this.universities.filter(u => u.id === id)[0];
            this.universities.splice(this.universities.indexOf(universityToDelete), 1);
          }
        })
    );
  }

  ngOnDestroy(): void {
    this.activeSubscriptions.forEach(s => s.unsubscribe());
    this.activeSubscriptions = [];
  }
}
