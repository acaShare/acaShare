import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { HttpClient } from '@angular/common/http';
import { Notification } from "./notification";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  areNotificationsShown = false;
  isNavbarShown = false;
  apiUrl = "api/v1/notifications";
  notifications: Notification[];

  constructor(
    private authorizeService: AuthorizeService,
    private http: HttpClient
    ) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.http
      .get<Notification[]>(this.apiUrl)
      .subscribe(notfs => this.notifications = notfs, error => console.log(error));
  }

  toggleNotifications() {
    this.areNotificationsShown = !this.areNotificationsShown;
  }

  toggleSidebar() {
    this.isNavbarShown = !this.isNavbarShown;
  }

  toggleFullNotificationContent(notification: Notification) {
    notification.showFullContent = !notification.showFullContent;
  }

  private maxLength: number = 100;

  shouldShowPointerOnNotification(notification: Notification) {
    return notification.content.length > this.maxLength;
  }

  shortenNotificationContent(notification: Notification): string {
    const notificationContent: string = notification.content;

    if (notification.showFullContent || notificationContent.length <= this.maxLength) {
      return notificationContent;
    }
    else {
      return notificationContent.substr(0, this.maxLength) + " [...]";
    }
  }
}
