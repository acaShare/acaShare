import { Component, OnInit } from '@angular/core';
import { Semester } from './semester';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-semesters-management',
  templateUrl: './semesters-management.component.html',
  styleUrls: ['./semesters-management.component.css']
})
export class SemestersManagementComponent implements OnInit {
  private apiPrefix = 'api/v1';
  private universityIdPlaceholder: string = '[_universityID-PLACEHOLDER_]';
  private departmentIdPlaceholder: string = '[_departmentID-PLACEHOLDER_]';
  private fetchApiUrl: string = `${this.apiPrefix}/universities/${this.universityIdPlaceholder}/departments/${this.departmentIdPlaceholder}/semesters`;
  semesters: Semester[];

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    const universityId = this.route.snapshot.paramMap.get('universityId');
    const departmentId = this.route.snapshot.paramMap.get('departmentId');
    const apiUrl = this.fetchApiUrl.replace(this.universityIdPlaceholder, universityId).replace(this.departmentIdPlaceholder, departmentId); 
    this.http
      .get<Semester[]>(apiUrl)
      .subscribe(d => this.semesters = d, error => console.log(error));
  }
}
