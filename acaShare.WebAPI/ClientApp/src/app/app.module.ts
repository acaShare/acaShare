import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { WelcomePageComponent } from './welcome-page/welcome-page.component';
import { ModeratorPanelComponent } from './moderator-panel/moderator-panel.component';
import { MaterialsToApproveComponent } from './materials-to-approve/materials-to-approve.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { PolishDatePipe } from './pipes/polish-date.pipe';
import { DeleteSuggestionsComponent } from './delete-suggestions/delete-suggestions.component';
import { AcaCollectionComponent } from './aca-collection/aca-collection.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    WelcomePageComponent,
    ModeratorPanelComponent,
    MaterialsToApproveComponent,
    PageNotFoundComponent,
    PolishDatePipe,
    DeleteSuggestionsComponent,
    AcaCollectionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: WelcomePageComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard] },
      { path: 'moderator-panel', 
        component: ModeratorPanelComponent,
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'materials-to-approve'},
          { path: 'materials-to-approve', component: MaterialsToApproveComponent },
          { path: 'delete-suggestions', component: DeleteSuggestionsComponent },
        ]
      },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
