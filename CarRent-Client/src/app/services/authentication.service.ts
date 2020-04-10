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
          // login successful if there's a jwt token in the response
          if (token.accessToken && token.refreshToken) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('accessToken', JSON.stringify(token));
            this.setCurrentUserToLocalStorage(
              this.getDecodedAccessToken(token.accessToken)
            );
          }
          this.IsUserLoggedIn.next(true);
          return token;
        })
      );
  }
  private getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }
  private setCurrentUserToLocalStorage(token: object): void {
    const keys = Object.keys(token);
    const result = {};

    for (const key of keys) {
      result[String(key).split('/').pop()] = token[key];
    }
    localStorage.setItem('currentUser', JSON.stringify(result));
  }
  public logout(): void {
    // remove user from local storage to log user out
    localStorage.removeItem('accessToken');
    localStorage.removeItem('currentUser');

    this.IsUserLoggedIn.next(false);
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
