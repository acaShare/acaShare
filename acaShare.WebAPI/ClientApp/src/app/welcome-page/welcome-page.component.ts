import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-welcome-page',
  templateUrl: './welcome-page.component.html',
  styleUrls: ['./welcome-page.component.css']
})
export class WelcomePageComponent implements OnInit {
  
  constructor(
    private authorizeService: AuthorizeService,
    private router: Router) { }

  ngOnInit() {
    if(this.authorizeService.isAuthenticated()) {
      this.router.navigate(["home"]);
    }
  }
}
