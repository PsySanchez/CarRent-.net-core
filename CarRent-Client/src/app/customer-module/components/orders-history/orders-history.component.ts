import { Component, OnInit, ViewChild } from '@angular/core';
import { OrderService } from 'src/app/services';
import { OrderModel, CarModel } from 'src/app/models';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-orders-history',
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.scss'],
})
export class OrdersHistoryComponent implements OnInit {
  public allCars: CarModel[];
  public dataSource: MatTableDataSource<any>;

  // MatPaginator Inputs
  public length = 0;
  public pageSize = 10;
  public pageSizeOptions: number[] = [5, 10, 25, 100];

  public displayedColumns: string[] = [
    'id',
    'carId',
    'fromDate',
    'toDate',
    'totalCost',
  ];

  constructor(private readonly orderService: OrderService) {}

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  ngOnInit(): void {
    this.orderService
      .getUserOrdersHistory('barny@gmail.com')
      .subscribe((orders: OrderModel[]) => {
        this.length = orders.length;
        this.dataSource = new MatTableDataSource<OrderModel>(orders);
        console.log(orders);
      });
  }

  public leave() {
    setTimeout(() => {
      console.log('leave');
    }, 1000);
  }

  public over(id: number) {
    console.log(id);
  }
}
