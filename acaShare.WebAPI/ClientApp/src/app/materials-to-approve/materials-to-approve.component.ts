import { Component, OnInit } from '@angular/core';
import { Material } from '../material';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-materials-to-approve',
  templateUrl: './materials-to-approve.component.html',
  styleUrls: ['./materials-to-approve.component.css']
})
export class MaterialsToApproveComponent implements OnInit {
  apiUrl: string = "api/v1/MaterialsToApprove"
  materials: Material[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http
      .get<Material[]>(this.apiUrl)
      .subscribe(materials => this.materials = materials, error => console.log(error));
  }

  approveMaterial(){

  }

  rejectMaterial(){
    
  }
}
