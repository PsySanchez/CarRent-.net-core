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
          this.navLinks = [
            {
              label: 'Info',
              path: '/customer/info',
            },
            {
              label: 'Orders History',
              path: '/customer/orders-history',
            },
          ];
        }
      });
    }, 1000);
  }
}
