import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ElementType } from './elementType';

@Injectable({
  providedIn: 'root'
})
export class UniversityTreeManagementService {
  private elementType: ElementType;

  constructor(private http: HttpClient) { }
}
