import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Subject } from './subject';
import { Semester } from '../semesters-management/semester';
import { Department } from '../departments-management/department';
import { University } from '../universities-management/university';

@Component({
  selector: 'app-subjects-management',
  templateUrl: './subjects-management.component.html',
  styleUrls: ['./subjects-management.component.css']
})
export class SubjectsManagementComponent implements OnInit {
  private apiPrefix = 'api/v1';
  private apiUrl: string = `api/v1/subjects`;
  private universityIdPlaceholder: string = '[_universityID-PLACEHOLDER_]';
  private departmentIdPlaceholder: string = '[_departmentID-PLACEHOLDER_]';
  private semesterIdPlaceholder: string = '[_semesterID-PLACEHOLDER_]';
  private fetchApiUrl: string = `${this.apiPrefix}/universities/${this.universityIdPlaceholder}/departments/${this.departmentIdPlaceholder}/semesters/${this.semesterIdPlaceholder}/subjects`;
  subjects: Subject[];
  universityId: string;
  departmentId: string;
  universityAbbreviation: string;
  departmentAbbreviation: string;
  semesterNumber: string;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.universityId = this.route.snapshot.paramMap.get('universityId');
    this.departmentId = this.route.snapshot.paramMap.get('departmentId');
    const semesterId = this.route.snapshot.paramMap.get('semesterId');

    this.http
      .get<University>(`${this.apiPrefix}/universities/${this.universityId}`)
      .subscribe(university => this.universityAbbreviation = university.abbreviation, error => console.log(error));
    this.http
      .get<Department>(`${this.apiPrefix}/universities/${this.universityId}/departments/${this.departmentId}`)
      .subscribe(dept => this.departmentAbbreviation = dept.abbreviation, error => console.log(error));
    this.http
      .get<Semester>(`${this.apiPrefix}/universities/${this.universityId}/departments/${this.departmentId}/semesters/${semesterId}`)
      .subscribe(sem => this.semesterNumber = sem.name, error => console.log(error));

    const apiUrl = this.fetchApiUrl
      .replace(this.universityIdPlaceholder, this.universityId)
      .replace(this.departmentIdPlaceholder, this.departmentId)
      .replace(this.semesterIdPlaceholder, semesterId);
      
    this.http
      .get<Subject[]>(apiUrl)
      .subscribe(d => this.subjects = d, error => console.log(error));
  }

  onApproveDeleteSubject(id: number) {
    this.http
      .delete(`${this.apiUrl}/${id}`)
      .subscribe(
        res => res,
        error => console.log(error),
        () => {
          const subjectToDelete = this.subjects.filter(u => u.id === id)[0];
          this.subjects.splice(this.subjects.indexOf(subjectToDelete), 1);
        }
    );
  }
}
