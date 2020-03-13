import { Component, OnInit } from '@angular/core';
import { University } from './university';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  apiUrl = "api/v1/UniversitiesManagement";
  universities: University[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<University[]>(this.apiUrl)
      .subscribe(universities => this.universities = universities, error => console.log(error));
  }  
}
