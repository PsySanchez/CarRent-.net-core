import { Component, OnInit, ViewChild } from '@angular/core';
import { CarsService } from 'src/app/services';
import { CarModel } from 'src/app/models';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { OrderComponent } from '../order/order.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.scss'],
})
export class CarsComponent implements OnInit {
  public allCars: CarModel[];
  public dataSource: MatTableDataSource<any>;

  // MatPaginator Inputs
  public length = 0;
  public pageSize = 10;
  public pageSizeOptions: number[] = [5, 10, 25, 100];

  public displayedColumns: string[] = [
    'manufacturer',
    'model',
    'pricePerDay',
    'image',
    'order',
  ];
  public imagesFolder: string;

  constructor(
    private readonly carsService: CarsService,
    private router: Router,
    private bottomSheet: MatBottomSheet,
    private toastr: ToastrService,
  ) {}

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  ngOnInit(): void {
    this.carsService.getAllCars().subscribe((cars: CarModel[]) => {
      this.length = cars.length;
      this.dataSource = new MatTableDataSource<CarModel>(cars);
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, 1000);
      this.imagesFolder = `${environment.apiUrl}/images`;
    });
  }

  public goToOrder(id: number): void {
    const token = localStorage.getItem('accessToken');
    if (token) {
      this.bottomSheet.open(OrderComponent, {
        data: { names: ['id', id] },
      });
    } else {
      this.toastr.error('You are not authorized!', 'Please Login.', {
        timeOut: 3000,
        positionClass: 'toast-top-right',
      });
    }
  }
}
