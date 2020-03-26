import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  private readonly suffix = " | acaShare.Angular";

  constructor(
    private titleService: Title,
    private router: Router,
    private activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this.router
      .events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(() => {
          let child = this.activatedRoute.firstChild;
          while (child.firstChild) {
            child = child.firstChild;
          }

          return child.snapshot.data['title'] || "";
        })
      )
      .subscribe((newTitle: string) => this.titleService.setTitle(newTitle + this.suffix));
  }
}
