import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Observable, of } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ CommonModule, RouterLink, RouterOutlet, MatButtonModule, MatIconModule, MatListModule, MatSidenavModule, MatToolbarModule ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'fakemyresume';
  pageLinks = [
    { label: 'Home', route: '/home' },
  ];

  loggedIn: Observable<boolean> = of(true);

  login() {

  }

  logout() {
    
  }
}
