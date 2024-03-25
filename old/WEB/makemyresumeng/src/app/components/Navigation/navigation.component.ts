import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private authService: MsalService) { }

  ngOnInit(): void {

  }

  isLoggedIn(): boolean {
    return this.authService.instance.getActiveAccount() != null;
  }

  login() {
    this.authService.loginPopup()
    .subscribe((response: AuthenticationResult) => {
      this.authService.instance.setActiveAccount(response.account);
    });
  }

  logout() {
    this.authService.logout();
  }

}
