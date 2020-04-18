import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  SearchComponent,
  Page404Component,
  HomeComponent,
  LoginComponent,
  RegisterComponent,
} from 'src/app/components/index';
import { AuthGuard, AuthAdminGuard, LogoutGuard } from './helpers';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'search', component: SearchComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: HomeComponent, canActivate: [LogoutGuard] },
  { path: 'register', component: RegisterComponent },
  { path: 'page404', component: Page404Component },
  {
    path: 'customer',
    loadChildren: () =>
      import('src/app/customer-module/customer.module').then(
        (m) => m.CustomerModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'admin',
    loadChildren: () =>
      import('./admin-module/admin/admin.module').then((m) => m.AdminModule),
    canActivate: [AuthAdminGuard],
  },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
