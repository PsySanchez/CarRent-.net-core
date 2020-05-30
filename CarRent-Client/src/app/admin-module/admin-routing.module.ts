import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddNewCarComponent, CustSearchComponent } from './components';

const routes: Routes = [
  { path: 'add-new-car', component: AddNewCarComponent },
  { path: 'cust-search', component: CustSearchComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
