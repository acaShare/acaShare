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
import { DeleteSuggestionApprovalDecisionComponent } from './delete-suggestion-approval-decision/delete-suggestion-approval-decision.component';
import { PolishFullDatePipe } from './pipes/polish-full-date.pipe';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { RejectDeleteSuggestionComponent } from './reject-delete-suggestion/reject-delete-suggestion.component';
import { UniversitiesManagementComponent } from './universities-management/universities-management.component';
import { DepartmentsManagementComponent } from './departments-management/departments-management.component';
import { SemestersManagementComponent } from './semesters-management/semesters-management.component';
import { SubjectsManagementComponent } from './subjects-management/subjects-management.component';
import { ManageUniversityTreeElementComponent } from './manage-university-tree-element/manage-university-tree-element.component';

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
    AcaCollectionComponent,
    DeleteSuggestionApprovalDecisionComponent,
    PolishFullDatePipe,
    ConfirmationDialogComponent,
    RejectDeleteSuggestionComponent,
    UniversitiesManagementComponent,
    DepartmentsManagementComponent,
    SemestersManagementComponent,
    SubjectsManagementComponent,
    ManageUniversityTreeElementComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: WelcomePageComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard] },
      { 
        path: 'moderator-panel', 
        component: ModeratorPanelComponent,
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'materials-to-approve'},
          { path: 'materials-to-approve', component: MaterialsToApproveComponent },
          { path: 'delete-suggestions', component: DeleteSuggestionsComponent },
          { path: 'delete-suggestions/delete-suggestion-approval-decision/:id', component: DeleteSuggestionApprovalDecisionComponent },
          { path: 'delete-suggestions/decline-delete-request/:id', component: RejectDeleteSuggestionComponent },
          { path: 'university-tree-management/universities', component: UniversitiesManagementComponent },
          { path: 'university-tree-management/universities/add-university', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:id/edit-university', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:id/departments', component: DepartmentsManagementComponent },
          { path: 'university-tree-management/universities/:id/departments/add-department', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:id/departments/:id/edit-department', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters', component: SemestersManagementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters/add-semester', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters/:id/edit-semester', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects', component: SubjectsManagementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects/add-subject', component: ManageUniversityTreeElementComponent },
          { path: 'university-tree-management/universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects/:id/edit-subject', component: ManageUniversityTreeElementComponent },
        ]
      },
      { path: '**', component: PageNotFoundComponent }
    ]),
    ApiAuthorizationModule
],
providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }