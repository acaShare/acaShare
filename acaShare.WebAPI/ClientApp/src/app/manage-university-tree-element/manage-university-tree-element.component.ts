import { Component, OnInit } from '@angular/core';
import { UniversityTreeElement } from './universityTreeElement';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UniversityTreeManagementService } from './university-tree-management.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-manage-university-tree-element',
  templateUrl: './manage-university-tree-element.component.html',
  styleUrls: ['./manage-university-tree-element.component.css']
})
export class ManageUniversityTreeElementComponent implements OnInit {
  private headerActionName: string;
  private whatToAddOrEdit: string;
  private backActionName: string;
  private submitActionName: string;
  private model: UniversityTreeElement;

  constructor(
    private http: HttpClient,
    private service: UniversityTreeManagementService,
    private route: ActivatedRoute,
    private location: Location) {
      this.model = new UniversityTreeElement();
    }

  ngOnInit() {
    let elementType: string;
    let action: string;
    this.route.url.subscribe(u => {
      action = u[u.length-1].path;
      
      if (action.startsWith('add')) {
        elementType = u[u.length-2].path;
        this.headerActionName = "Dodawanie";
        this.submitActionName = "Dodaj";
      }
      else if (action.startsWith('edit')) {
        elementType = u[u.length-3].path;
        this.headerActionName = "Edycja";
        this.submitActionName = "Zapisz";

        const id = this.route.snapshot.paramMap.get('id');
        const routeName = elementType === 'subjects' ? 'lessons' : elementType;
        const apiUrl = `api/v1/${routeName}Management/${id}`;
        this.http
          .get<UniversityTreeElement>(apiUrl)
          .subscribe(ute => this.model = ute, error => console.log(error));
      }
      else {
        throw new Error('error -> manage-university-tree-element.component.ts [line: 37]');
      }

      switch(elementType) {
        case "universities":
          this.backActionName = "uczelni";
          this.whatToAddOrEdit = "uczelni";
          break;
        case "departments":
          this.backActionName = "wydziałów";
          this.whatToAddOrEdit = "wydziału";
          break;
        case "semesters":
          throw new Error('route not supported error -> manage-university-tree-element.component.ts [line: 37]');
        case "subjects":
          this.backActionName = "przedmiotów";
          this.whatToAddOrEdit = "przedmiotu";
          break;
        default:
          throw new Error('route not supported error -> manage-university-tree-element.component.ts [line: 37]');
      }
    });
  }

  goToPrevPage() {
    this.location.back();
  }
}
