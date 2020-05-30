import { Injectable, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import { FormGroup } from '@angular/forms';
import { UserHelper } from '../helpers/user.helper';

@Injectable({ providedIn: 'root' })
export class AuthService {
  @Output() IsUserLoggedIn = new BehaviorSubject(0);

  constructor(private http: HttpClient) {
    this.checkIsUserLoggedIn();
  }

  public login(loginForm: FormGroup): Observable<any> {
    return this.http
      .post<any>(
        `${environment.apiUrl}/authentication`,
        UserHelper.getFormData(loginForm)
      )
      .pipe(
        map((token) => {
          this.setCurrentUserToLocalStorage(token);
          return token;
        })
      );
  }

  public refreshToken(): Observable<any> {
    const oldToken = this.getJwtToken();

    if (oldToken) {
      const refreshToken = new FormData();
      refreshToken.append('accessToken', oldToken.accessToken);
      refreshToken.append('refreshToken', oldToken.refreshToken);
      this.removeToken();
      return this.http
        .post<any>(`${environment.apiUrl}/authentication/refresh`, refreshToken)
        .pipe(
          map((token: any) => {
            this.setCurrentUserToLocalStorage(token);
            return token;
          })
        );
    }
  }

  private removeToken(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('currentUser');
  }

  public logout(): void {
    // remove user from local storage to log user out
    this.removeToken();
    this.IsUserLoggedIn.next(0);
  }

  private getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }

  private setCurrentUserToLocalStorage(token: any): void {
    if (token.accessToken && token.refreshToken) {
      localStorage.setItem('accessToken', JSON.stringify(token));

      const userData = this.getDecodedAccessToken(token.accessToken);

      const keys = Object.keys(userData);
      const result = {};
      for (const key of keys) {
        result[String(key).split('/').pop()] = userData[key];
        // Check if role is exist and set het:
      }
      localStorage.setItem('currentUser', JSON.stringify(result));
      this.IsUserLoggedIn.next(1);
    }
  }

  public getUserRole(): string {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    if (user) {
      return user.role;
    }
    return null;
  }

  private checkIsUserLoggedIn(): void {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    if (user) {
      this.IsUserLoggedIn.next(1);
    }
  }

  public getJwtToken(): any {
    return JSON.parse(localStorage.getItem('accessToken'));
  }

  public getCurrentUser(): any {
    return JSON.parse(localStorage.getItem('currentUser'));
  }
}
