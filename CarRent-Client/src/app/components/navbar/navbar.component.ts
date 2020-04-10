import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  navLinks: any[];
  constructor() {}

  ngOnInit(): void {
    setTimeout(() => {
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
    }, 1000);
  }
}
