import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { CarsService, OrderService } from 'src/app/services';
import { CarModel } from 'src/app/models';
import { environment } from 'src/environments/environment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
})
export class OrderComponent implements OnInit {
  public car: CarModel;
  public imagesFolder: string;
  public minDateFrom = new Date();
  public minDateTo = new Date();
  public orderForm: FormGroup;
  private currentUser: object;

  constructor(
    private bottomSheetRef: MatBottomSheetRef<OrderComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private carsService: CarsService,
    private fb: FormBuilder,
    private orderService: OrderService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    const result = Object.values(this.data).pop();
    this.carsService
      .getOneCars(Object.values(result).pop())
      .subscribe((car) => {
        this.car = car;
      });
    this.imagesFolder = `${environment.apiUrl}/images`;
    this.orderForm = this.fb.group({
      email: '',
      fromDate: [new Date(), Validators.required],
      toDate: [new Date(), Validators.required],
      carId: Number,
      totalCost: Number,
    });
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  onSubmit(event: MouseEvent): void {
    this.setValueToForm();
    const text = `Total cost: ${this.orderForm.controls.totalCost.value} $`;

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '350px',
      data: { text: `${text}`, fromDate: `${this.orderForm.controls.fromDate.value}`, toDate: `${this.orderForm.controls.toDate.value}` },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.orderService.addNewOrder(this.orderForm).subscribe(
          () => {
            this.toastr.success('Order completed successfully!', '', {
              timeOut: 3000,
              positionClass: 'toast-top-right',
            });
            this.bottomSheetRef.dismiss();
            event.preventDefault();
          },
          (error) => {
            this.bottomSheetRef.dismiss();
            event.preventDefault();
          }
        );
      }
    });
  }

  private getTotalCost(): number {
    const fromDate = this.orderForm.controls.fromDate.value.getTime();
    const toDate = this.orderForm.controls.toDate.value.getTime();
    if (fromDate !== toDate) {
      return (
        Math.floor((toDate - fromDate) / 1000 / 86400) * this.car.pricePerDay
      );
    }
    return this.car.pricePerDay;
  }

  public setDateToForm(): void {
    if (this.orderForm.controls.fromDate.valueChanges) {
      this.minDateTo = new Date(this.orderForm.controls.fromDate.value);
      if (
        !this.orderForm.controls.toDate.value ||
        this.orderForm.controls.toDate.value < this.minDateTo
      ) {
        this.orderForm.controls.toDate.setValue(this.minDateTo);
      }
    }
  }

  private setValueToForm(): void {
    this.orderForm.controls.email.setValue(Object.values(this.currentUser)[0]);
    this.orderForm.controls.carId.setValue(this.car.id);
    this.orderForm.controls.totalCost.setValue(this.getTotalCost());
    this.orderForm.controls.fromDate.setValue(
      new Date(this.orderForm.controls.fromDate.value).toUTCString()
    );
    this.orderForm.controls.toDate.setValue(
      new Date(this.orderForm.controls.toDate.value).toUTCString()
    );
  }

  private openDialog(): void {}
}
