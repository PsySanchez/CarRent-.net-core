import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  navLinks: any[];
  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.authService.IsUserLoggedIn.subscribe((isLoggin) => {
        if (isLoggin) {
          const userPath = this.authService.getUserRole();
          this.navLinks = [
            {
              label: 'Home',
              path: '/home',
            },
            {
              label: 'Search',
              path: '/search',
            },
            {
              label: 'Logout',
              path: '/logout',
            },
            {
              label: 'My Info',
              path: `${userPath}`,
            },
          ];
        } else {
          this.navLinks = [
            {
              label: 'Home',
              path: '/home',
            },
            {
              label: 'Search',
              path: '/search',
            },
            {
              label: 'Login',
              path: '/login',
            },
          ];
        }
      });
    }, 1000);
  }
}
