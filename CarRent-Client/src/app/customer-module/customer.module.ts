import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { InfoComponent } from './components/info/info.component';
import { OrdersHistoryComponent } from './components/orders-history/orders-history.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';

@NgModule({
  declarations: [CustomerComponent, InfoComponent, OrdersHistoryComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    MatTabsModule,
    MatMenuModule,
    MatButtonModule,
    MatPaginatorModule,
    MatTableModule,
  ],
  exports: [CustomerComponent],
})
export class CustomerModule {}
