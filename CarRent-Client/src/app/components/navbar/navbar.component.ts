import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  public navLinks: any[];
  public userPath = '';
  public isLoggin = false;
  public userMenu = [];
  constructor(private readonly authService: AuthService) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.authService.IsUserLoggedIn.subscribe((isLoggin) => {
        if (isLoggin) {
          this.isLoggin = true;
          this.userPath = this.authService.getUserRole();
          this.setUserMenu(this.userPath);
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
          ];
        } else {
          this.isLoggin = false;
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

  private setUserMenu(userRole: string) {
    this.userMenu = [];

    if (userRole === 'customer') {
      this.userMenu.push({ label: 'Info', path: '/customer/info' });
      this.userMenu.push({
        label: 'History',
        path: '/customer/orders-history',
      });
    } else if (userRole === 'admin') {
      this.userMenu.push({ label: 'Add new car', path: '/admin/add-new-car' });
      this.userMenu.push({ label: 'Search customer', path: '/admin/cust-search' });
    }
  }
}
