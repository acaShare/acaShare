import { Component, OnInit } from '@angular/core';
import { UniversityTreeElement } from './universityTreeElement';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-manage-university-tree-element',
  templateUrl: './manage-university-tree-element.component.html',
  styleUrls: ['./manage-university-tree-element.component.css']
})
export class ManageUniversityTreeElementComponent implements OnInit {
  headerActionName: string;
  whatToAddOrEdit: string;
  backActionName: string;
  submitActionName: string;
  model: UniversityTreeElement;

  private action: string;
  private apiBaseUrl: string = "api/v1/universities";
  private getAndPutApiUrl: string = this.apiBaseUrl;
  private postApiUrl: string = this.apiBaseUrl;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private location: Location) {
      this.model = new UniversityTreeElement();
    }

  ngOnInit() {
    let elementType: string;
    this.route.url.subscribe(u => {
      this.action = u[u.length-1].path;
      
      if (this.action.startsWith('add')) {
        elementType = u[u.length-2].path;
        this.headerActionName = "Dodawanie";
        this.submitActionName = "Dodaj";
      }
      else if (this.action.startsWith('edit')) {
        elementType = u[u.length-3].path;
        this.headerActionName = "Edycja";
        this.submitActionName = "Zapisz";
      }
      else {
        throw new Error('error -> manage-university-tree-element.component.ts [line: 37]');
      }

      switch(elementType) {
        case "universities":
          this.backActionName = "uczelni";
          this.whatToAddOrEdit = "uczelni";

          if (this.action.startsWith('edit')) {
            const universityId = this.route.snapshot.paramMap.get('id');
            this.getAndPutApiUrl = `${this.getAndPutApiUrl}/${universityId}`;
            this.http
              .get<UniversityTreeElement>(this.getAndPutApiUrl)
              .subscribe(ute => this.model = ute, error => console.log(error));
          }
          break;
        case "departments":
          this.backActionName = "wydziałów";
          this.whatToAddOrEdit = "wydziału";

          if (this.action.startsWith('add')) {
            const universityId = this.route.snapshot.paramMap.get('id');
            this.postApiUrl = `${this.apiBaseUrl}/${universityId}/departments`;
          }
          else if (this.action.startsWith('edit')) {
            const universityId = this.route.snapshot.paramMap.get('universityId');
            const departmentId = this.route.snapshot.paramMap.get('departmentId');
            this.getAndPutApiUrl = `${this.getAndPutApiUrl}/${universityId}/departments/${departmentId}`;
            this.http
              .get<UniversityTreeElement>(this.getAndPutApiUrl)
              .subscribe(ute => this.model = ute, error => console.log(error));
          }

          break;
        case "semesters":
          throw new Error('route not supported error -> manage-university-tree-element.component.ts [line: 37]');
        case "subjects":
          this.backActionName = "przedmiotów";
          this.whatToAddOrEdit = "przedmiotu";

          if (this.action.startsWith('add')) {
            const universityId = this.route.snapshot.paramMap.get('universityId');
            const departmentId = this.route.snapshot.paramMap.get('departmentId');
            const semesterId = this.route.snapshot.paramMap.get('semesterId');
            this.postApiUrl = `${this.apiBaseUrl}/${universityId}/departments/${departmentId}/semesters/${semesterId}/subjects`;
          }
          else if (this.action.startsWith('edit')) {
            const universityId = this.route.snapshot.paramMap.get('universityId');
            const departmentId = this.route.snapshot.paramMap.get('departmentId');
            const semesterId = this.route.snapshot.paramMap.get('semesterId');
            const subjectId = this.route.snapshot.paramMap.get('subjectId');
            this.getAndPutApiUrl = `${this.getAndPutApiUrl}/${universityId}/departments/${departmentId}/semesters/${semesterId}/subjects/${subjectId}`;
            this.http
              .get<UniversityTreeElement>(this.getAndPutApiUrl)
              .subscribe(ute => this.model = ute, error => console.log(error));
          }
          break;
        default:
          throw new Error('route not supported error -> manage-university-tree-element.component.ts [line: 37]');
      }
    });
  }

  goToPrevPage() {
    this.location.back();
  }

  onSubmit() {
    if (this.action.startsWith('add')) {
      this.http
        .post<UniversityTreeElement>(this.postApiUrl, this.model)
        .subscribe();
    }
    else if (this.action.startsWith('edit')) {
      this.http
        .put<UniversityTreeElement>(this.getAndPutApiUrl, this.model)
        .subscribe();
    }

    this.goToPrevPage();
  }
}
