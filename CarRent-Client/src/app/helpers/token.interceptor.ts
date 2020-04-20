import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const accessToken = this.authService.getJwtToken();
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (accessToken && !request.url.includes('cars')) {
      if (currentUser.exp < Math.floor(Date.now() / 1000)) {
        return this.authService.refreshToken().pipe(
          switchMap((token: any) => {
            return next.handle(this.addToken(request, token));
          })
        );
      } else {
        return next.handle(this.addToken(request, accessToken));
      }
    } else {
      return next.handle(request);
    }
  }

  private addToken(request: HttpRequest<any>, token: any): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token.accessToken}`,
      },
    });
  }
}
