import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { InfoComponent, OrdersHistoryComponent } from './components';
import { CustomerComponent } from './customer.component';

const routes: Routes = [
  { path: '', component: CustomerComponent },
  { path: 'info', component: InfoComponent },
  { path: 'orders-history', component: OrdersHistoryComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
