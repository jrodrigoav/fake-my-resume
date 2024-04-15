import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { ResumesComponent } from './pages/resumes/resumes.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  { path: 'resumes', component: ResumesComponent, canActivate: [] },
  { path: 'admin-panel', component: AdminPanelComponent, canActivate: [] },
  { path: '**', component: PageNotFoundComponent },
];
