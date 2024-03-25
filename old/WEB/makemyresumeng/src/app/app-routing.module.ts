import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { BrowserUtils } from '@azure/msal-browser';

import { HomeComponent } from './pages/home/home.component';
import { LoginFailedComponent } from './pages/login-failed/login-failed.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { TestUnicornApiComponent } from './pages/test-unicorn-api/test-unicorn-api.component';
import { ResumesComponent } from './pages/resumes/resumes.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, },
  { path: 'resumes', component: ResumesComponent, canActivate: [MsalGuard] },
  { path: 'admin-panel', component: AdminPanelComponent, canActivate: [MsalGuard] },
  { path: 'test', component: TestUnicornApiComponent, canActivate: [MsalGuard] },
  { path: 'login-failed', component: LoginFailedComponent },
  { path: '**', component: PageNotFoundComponent },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
       // Don't perform initial navigation in iframes or popups
       // Set to enabledBlocking to use Angular Universal
      initialNavigation: !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup() ? 'enabledNonBlocking' : 'disabled',
      enableTracing: false }) // <-- debugging purposes only)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
