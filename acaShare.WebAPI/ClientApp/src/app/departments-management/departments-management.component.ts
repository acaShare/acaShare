import { Component, OnInit } from '@angular/core';
import { Department } from './department';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { University } from '../universities-management/university';

@Component({
  selector: 'app-departments-management',
  templateUrl: './departments-management.component.html',
  styleUrls: ['./departments-management.component.css']
})
export class DepartmentsManagementComponent implements OnInit {
  private apiPrefix = 'api/v1';
  private apiUrl: string = `api/v1/departments`;
  private idPlaceholder: string = '[_ID-PLACEHOLDER_]';
  private fetchApiUrl: string = `${this.apiPrefix}/universities/${this.idPlaceholder}/departments`;
  departments: Department[];
  universityAbbreviation: string;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    const universityId = this.route.snapshot.paramMap.get('id');
    
    this.http
      .get<University>(`${this.apiPrefix}/universities/${universityId}`)
      .subscribe(university => this.universityAbbreviation = university.abbreviation, error => console.log(error));

    this.http
      .get<Department[]>(this.fetchApiUrl.replace(this.idPlaceholder, universityId))
      .subscribe(d => this.departments = d, error => console.log(error));
  }

  onApproveDeleteDepartment(id: number) {
    this.http
      .delete(`${this.apiUrl}/${id}`)
      .subscribe(
        res => res,
        error => console.log(error),
        () => {
          const departmentToDelete = this.departments.filter(u => u.id === id)[0];
          this.departments.splice(this.departments.indexOf(departmentToDelete), 1);
        }
    );
  }
}
