import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogBoxComponent } from '../components/dialog-box/dialog-box.component';
import {
  Observable,
  of,
} from 'rxjs';
import { AuthService } from '../services';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class LogoutGuard implements CanActivate {
  constructor(
    private router: Router,
    public dialog: MatDialog,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  // Can Activate with Observable and DialogBox:
  public canActivate(): Observable<any> {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '250px',
      data: { text: 'Do you want to log out?' },
    });
    return of(
      dialogRef.afterClosed().subscribe((result: any) => {
        if (result) {
          this.authService.logout();
          this.toastr.success('You are logged out.', '', {
            timeOut: 3000,
            positionClass: 'toast-top-right',
          });
          setTimeout(() => {
            this.router.navigate(['/home']);
          }, 3000);
          return true;
        }
      })
    );
  }
}
