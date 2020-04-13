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
import { AuthenticationService } from '../services';
import { ToastrService } from 'ngx-toastr';
import {
  MatDialog,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { DialogBoxComponent } from '../components/dialog-box/dialog-box.component';
// import { CacheService } from 'src/app/services/cache.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private readonly router: Router,
    private readonly authService: AuthenticationService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {}

  // Intercepts HttpRequest or HttpResponse and handles them.
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 500 || err.status === 0) {
          // this.cache.loadingSpinner = false;
          this.router.navigate(['/page404']);
        }
        if (err.status === 401) {
          if (localStorage.getItem('accessToken')) {
            this.openDialog();
          } else {
          }
        }
        return throwError(err.error);
      })
    );
  }

  private openDialog(): void {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      data: { text: 'Connection end, reconnect?' },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.authService.refreshToken().subscribe(
          () => {
            this.toastr.success('Connection refresh.', '', {
              timeOut: 3000,
              positionClass: 'toast-top-right',
            });
          },
          (error) => {
            this.toastr.error('You are not authorized!', 'Please relogin.', {
              timeOut: 3000,
              positionClass: 'toast-top-right',
            });
            setTimeout(() => this.router.navigate(['/login']), 3000);
          }
        );
      }
    });
  }
}
