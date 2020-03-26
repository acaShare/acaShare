import { Component, OnInit } from '@angular/core';
import { Semester } from './semester';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { University } from '../universities-management/university';
import { Department } from '../departments-management/department';

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
  universityId: string;
  universityAbbreviation: string;
  departmentAbbreviation: string;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.universityId = this.route.snapshot.paramMap.get('universityId');
    const departmentId = this.route.snapshot.paramMap.get('departmentId');

    this.http
      .get<University>(`${this.apiPrefix}/universities/${this.universityId}`)
      .subscribe(university => this.universityAbbreviation = university.abbreviation, error => console.log(error));
    this.http
      .get<Department>(`${this.apiPrefix}/universities/${this.universityId}/departments/${departmentId}`)
      .subscribe(dept => this.departmentAbbreviation = dept.abbreviation, error => console.log(error));

    const apiUrl = this.fetchApiUrl.replace(this.universityIdPlaceholder, this.universityId).replace(this.departmentIdPlaceholder, departmentId); 
    this.http
      .get<Semester[]>(apiUrl)
      .subscribe(s => this.semesters = s, error => console.log(error));
  }
}
