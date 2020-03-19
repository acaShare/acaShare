import { Component, OnInit } from '@angular/core';
import { University } from './university';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-universities-management',
  templateUrl: './universities-management.component.html',
  styleUrls: ['./universities-management.component.css']
})
export class UniversitiesManagementComponent implements OnInit {
  apiUrl: string = "api/v1/UniversitiesManagement";
  universities: University[];
  isAdmin = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http
      .get<University[]>(this.apiUrl)
      .subscribe(u => this.universities = u, error => console.log(error));
  }

  onApproveDeleteUniversity(id: number) {
    this.http
      .delete(`${this.apiUrl}/${id}`)
      .subscribe(
        res => res,
        error => console.log(error),
        () => {
          const universityToDelete = this.universities.filter(u => u.id === id)[0];
          this.universities.splice(this.universities.indexOf(universityToDelete, 1));
        }
    );
  }
}
