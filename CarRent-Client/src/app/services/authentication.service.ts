import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Subject, Observable } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import { FormGroup } from '@angular/forms';
import { UserHelper } from '../helpers/user.helper';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  public IsUserLoggedIn: Subject<boolean> = new Subject<boolean>();

  constructor(private http: HttpClient) {}

  public login(loginForm: FormGroup): Observable<any> {
    return this.http
      .post<any>(
        `${environment.apiUrl}/authentication`,
        UserHelper.getFormData(loginForm)
      )
      .pipe(
        map((token) => {
          this.setCurrentUserToLocalStorage(token);
          this.IsUserLoggedIn.next(true);
          return token;
        })
      );
  }

  public refreshToken(): Observable<any> {
    const oldToken = JSON.parse(localStorage.getItem('accessToken'));
    const refreshToken = new FormData();
    refreshToken.append('accessToken', oldToken.accessToken);
    refreshToken.append('refreshToken', oldToken.refreshToken);
    this.logout();

    return this.http
      .post<any>(`${environment.apiUrl}/authentication/refresh`, refreshToken)
      .pipe(
        map((token) => {
          this.setCurrentUserToLocalStorage(token);
          this.IsUserLoggedIn.next(true);
        })
      );
  }

  public logout(): void {
    // remove user from local storage to log user out
    localStorage.removeItem('accessToken');
    localStorage.removeItem('currentUser');

    this.IsUserLoggedIn.next(false);
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
      }
      localStorage.setItem('currentUser', JSON.stringify(result));
    }
  }

  // var result = Object.keys(token).map((key) => [
  //   String(key).split("/").pop(),
  //   token[key],
  // ]);
  // for (let i = 0; i < keys.length; i++) {
  //     const key = keys[i];
  //     result[String(key).split('/').pop()] = token[key];
  //   }
}
