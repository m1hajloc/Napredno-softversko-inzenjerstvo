import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { Clicks } from './pages/clicks/clicks';
import { ManageUrls } from './pages/manage-urls/manage-urls';
import { CreateUrl } from './pages/create-url/create-url';
import { MatIcon } from '@angular/material/icon';
import { MatToolbar } from '@angular/material/toolbar';
import { Auth } from './service/auth';

@Component({
  selector: 'app-root',
  imports: [
    CommonModule,
    MatTabsModule,
    MatToolbar,
    Home,
    CreateUrl,
    ManageUrls,
    Clicks,
    Login,
    MatIcon,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  constructor(private authService: Auth) {}
  ngOnInit() {
    this.authService.isLoggedIn$.subscribe((value) => (this.isLoggedIn = value));
  }
  public isLoggedIn: boolean = true;
  protected readonly title = signal('urlShortenerClient');

  logout() {
    this.isLoggedIn = false;
  }
  login() {
    this.isLoggedIn = true;
  }
}
