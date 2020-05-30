import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MatIconModule} from '@angular/material/icon';
import {
  SearchComponent,
  HomeComponent,
  Page404Component,
  NavbarComponent,
  LoginComponent,
  RegisterComponent,
  CarsComponent,
  OrderComponent,
} from './components';
import {
  CarsService,
  UserService,
  CacheService,
  AuthService,
  OrderService,
  AdminService,
} from './services/index';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  TokenInterceptor,
  ErrorInterceptor,
  AuthGuard,
  AuthAdminGuard,
  LogoutGuard,
} from './helpers';
import { DialogBoxComponent } from './components/dialog-box/dialog-box.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    Page404Component,
    SearchComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    CarsComponent,
    OrderComponent,
    DialogBoxComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTabsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    HttpClientModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatListModule,
    MatBottomSheetModule,
    MatDatepickerModule,
    MatNativeDateModule,
    ToastrModule.forRoot(),
    MatDialogModule,
    MatMenuModule,
    MatIconModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthGuard,
    AuthAdminGuard,
    LogoutGuard,
    AuthService,
    UserService,
    CarsService,
    CacheService,
    MatDatepickerModule,
    OrderService,
    AdminService,
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
