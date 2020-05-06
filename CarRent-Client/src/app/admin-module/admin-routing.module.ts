import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AddNewCarComponent } from './components';

const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: 'add-new-car', component: AddNewCarComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
