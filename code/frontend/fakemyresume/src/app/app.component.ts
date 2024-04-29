import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Subject, filter } from 'rxjs';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MsalBroadcastService, MsalModule, MsalService } from '@azure/msal-angular';
import { AccountInfo, EventType } from '@azure/msal-browser';
import { HeaderComponent } from './layout/header/header.component';
import { UserService } from './services/user/user.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ CommonModule, RouterLink, RouterOutlet, MatListModule, MatSidenavModule, MsalModule, HeaderComponent ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'fakemyresume';
  pageLinks = [
    { label: 'Home', route: '/home' },
    { label: 'My Resumes', route: '/resumes' },
    { label: 'Admin Panel', route: '/admin-panel' },
  ];

  loggedIn = new Subject<boolean>();

  constructor(private authService: MsalService, private msalBroadcastService: MsalBroadcastService, private userService: UserService) {
    this.loggedIn.pipe(filter(state => state === true)).subscribe((_) => {
      this.userService.loadCurrentUser();
    });
    this.authService.initialize().subscribe(() => {
      this.onAuthInitialize();
    });
  }

  onAuthInitialize() {
    const msalInstance = this.authService.instance;
    // Account selection logic is app dependent. Adjust as needed for different use cases.
    // Set active acccount on page load
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length > 0) {
      msalInstance.setActiveAccount(accounts[0]);
    }
    this.loggedIn.next(this.authService.instance.getActiveAccount() != null);

    this.msalBroadcastService.msalSubject$
    .pipe(filter(e => e.eventType === EventType.LOGIN_SUCCESS))
    .subscribe(event => {
      // set active account after redirect
      const accountInfo = event.payload as AccountInfo;
      msalInstance.setActiveAccount(accountInfo);
      this.loggedIn.next(this.authService.instance.getActiveAccount() != null);
    });

    // handle auth redired/do all initial setup for msal
    msalInstance.handleRedirectPromise().then(authResult => {
      // TODO: Handle redirect promise
    }).catch(err => {
      // TODO: Handle errors
      console.log(err);
    });
  }

  login() {
    this.authService.loginPopup();
  }

  logout() {
    this.authService.logout();
  }
}
