import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {
  SearchComponent,
  Page404Component,
  HomeComponent,
  LoginComponent,
  RegisterComponent,
} from 'src/app/components/index';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'search', component: SearchComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: 'page404', component: Page404Component },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
