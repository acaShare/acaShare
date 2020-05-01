import { BrowserModule, Title } from '@angular/platform-browser';
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
import { RegulationsComponent } from './regulations/regulations.component';

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
    RegulationsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: WelcomePageComponent, pathMatch: 'full', data: { title: 'Witamy' } },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard], data: { title: 'Strona główna' }  },
      { 
        path: 'moderator-panel', 
        component: ModeratorPanelComponent,
        canActivate: [AuthorizeGuard],
        children: [
          { path: '', pathMatch: 'full', redirectTo: 'materials-to-approve' },
          { path: 'materials-to-approve', component: MaterialsToApproveComponent, data: { title: 'Materiały oczekujące na zatwierdzenie' }  },
          { path: 'delete-suggestions', component: DeleteSuggestionsComponent, data: { title: 'Sugestie usunięcia' }  },
          { path: 'delete-suggestions/delete-suggestion-approval-decision/:id', component: DeleteSuggestionApprovalDecisionComponent, data: { title: 'Podsumowanie sugestii usunięcia' }  },
          { path: 'delete-suggestions/decline-delete-request/:id', component: RejectDeleteSuggestionComponent, data: { title: 'Odrzucenie sugestii usunięcia materiału' }  },
          {
            path: 'university-tree-management',
            children: [
              { 
                path: '', 
                redirectTo: 'universities', 
                pathMatch: 'full' 
              },
              { path: 'universities', component: UniversitiesManagementComponent, data: { title: 'Uczelnie' }  },
              { path: 'universities/add-university', component: ManageUniversityTreeElementComponent, data: { title: 'Dodawnie uczelni' }  },
              { path: 'universities/:id/edit-university', component: ManageUniversityTreeElementComponent, data: { title: 'Edycja uczelni' }  },
              { path: 'universities/:id/departments', component: DepartmentsManagementComponent, data: { title: 'Wydziały' }  },
              { path: 'universities/:id/departments/add-department', component: ManageUniversityTreeElementComponent, data: { title: 'Dodawnie wydziału' }  },
              { path: 'universities/:universityId/departments/:departmentId/edit-department', component: ManageUniversityTreeElementComponent, data: { title: 'Edycja wydziału' }  },
              { path: 'universities/:universityId/departments/:departmentId/semesters', component: SemestersManagementComponent, data: { title: 'Semestry' }  },
              { path: 'universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects', component: SubjectsManagementComponent, data: { title: 'Przedmioty' }  },
              { path: 'universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects/add-subject', component: ManageUniversityTreeElementComponent, data: { title: 'Dodawnie przedmiotu' }  },
              { path: 'universities/:universityId/departments/:departmentId/semesters/:semesterId/subjects/:subjectId/edit-subject', component: ManageUniversityTreeElementComponent, data: { title: 'Edycja przedmiotu' }  },
            ]
          },
          { path: 'regulations', component: RegulationsComponent, data: { title: "Regulamin" } }
        ]
      },
      { path: '**', component: PageNotFoundComponent }
    ]),
    ApiAuthorizationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    Title
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
