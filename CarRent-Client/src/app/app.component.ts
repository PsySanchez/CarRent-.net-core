import { Component, OnInit } from '@angular/core';
import { AuthService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'CarRent-Client';
  constructor(private readonly authService: AuthService) {}
  ngOnInit(){
    this.authService.refreshToken().subscribe();
  }
}
