import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { ResumesComponent } from './pages/resumes/resumes.component';
import { MsalGuard } from '@azure/msal-angular';
import { ResumeFormComponent } from './pages/resumes/resume-form/resume-form.component';
import { NotesComponent } from './pages/notes/notes.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  {
    path: 'resumes',
    canActivate: [MsalGuard],
    children: [
      { path: '', component: ResumesComponent },
      { path: ':resumeId', component: ResumeFormComponent },
    ]
  },
  { path: 'notes', canActivate: [MsalGuard], component: NotesComponent },
  { path: '**', component: PageNotFoundComponent },
];
