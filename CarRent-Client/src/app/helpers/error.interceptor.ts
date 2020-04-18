import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services';

// import { CacheService } from 'src/app/services/cache.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private readonly router: Router,
    private toastr: ToastrService,
    private readonly authService: AuthService
  ) {}

  // Intercepts HttpRequest or HttpResponse and handles them.
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 500 || err.status === 0) {
          this.router.navigate(['/page404']);
        }
        if (err.status === 401) {
          if (this.router.url !== '/login') {
            this.authService.logout();
            this.toastr.error('You are not authorized!', 'Please relogin.', {
              timeOut: 3000,
              positionClass: 'toast-top-right',
            });
            setTimeout(() => this.router.navigate(['/login']), 3000);
          }
        }

        return throwError(err.error);
      })
    );
  }
}

// if (currentUser.exp < Math.floor(Date.now() / 1000)) {
//   this.authService.refreshToken().subscribe(
//     (token: any) => {
//       if (token) {
//         // your connection is over, reconnecting
//       }
//     }
