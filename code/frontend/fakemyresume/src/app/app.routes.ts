import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { ResumesComponent } from './pages/resumes/resumes.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { TestUnicornApiComponent } from './pages/test-unicorn-api/test-unicorn-api.component';
import { MsalGuard } from '@azure/msal-angular';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  { path: 'resumes', component: ResumesComponent, canActivate: [MsalGuard] },
  { path: 'admin-panel', component: AdminPanelComponent, canActivate: [MsalGuard] },
  { path: 'test', component: TestUnicornApiComponent, canActivate: [MsalGuard] },
  { path: '**', component: PageNotFoundComponent },
];
