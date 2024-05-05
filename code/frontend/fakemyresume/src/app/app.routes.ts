import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { ResumesComponent } from './pages/resumes/resumes.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { MsalGuard } from '@azure/msal-angular';
import { userGuard } from './guards/user/user.guard';
import { ResumeFormComponent } from './pages/resumes/resume-form/resume-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  {
    path: 'resumes',
    canActivate: [MsalGuard, userGuard],
    children: [
      { path: '', component: ResumesComponent },
      { path: ':resumeId', component: ResumeFormComponent },
    ]
  },
  { path: 'admin-panel', component: AdminPanelComponent, canActivate: [MsalGuard, userGuard] },
  { path: '**', component: PageNotFoundComponent },
];
