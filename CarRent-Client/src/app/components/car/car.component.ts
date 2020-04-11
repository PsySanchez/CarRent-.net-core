import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { CarsService } from 'src/app/services';
import { CarModel } from 'src/app/models';
import { environment } from 'src/environments/environment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.scss'],
})
export class CarComponent implements OnInit {
  public car: CarModel;
  public imagesFolder: string;
  public minDateFrom = new Date();
  public minDateTo = new Date();
  public orderForm: FormGroup;
  private currentUser: object;

  constructor(
    private bottomSheetRef: MatBottomSheetRef<CarComponent>,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private carsService: CarsService,
    private fb: FormBuilder
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
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
      carId: Number,
      totalCost: Number,
    });
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  onSubmit(event: MouseEvent): void {
    this.orderForm.controls.email.setValue(Object.values(this.currentUser)[0]);
    this.orderForm.controls.carId.setValue(this.car.id);
    this.orderForm.controls.totalCost.setValue(this.getTotalCost());
    this.bottomSheetRef.dismiss();
    event.preventDefault();
    console.log(this.orderForm);
  }

  getTotalCost() {
    const fromDate = this.orderForm.controls.fromDate.value.getTime();
    const toDate = this.orderForm.controls.toDate.value.getTime();
    if (fromDate !== toDate) {
      return (
        Math.floor((toDate - fromDate) / 1000 / 86400) *
        this.car.pricePerDay
      );
    }
    return this.car.pricePerDay;
  }

  public setDateToForm(): void {
    if (this.orderForm.controls.fromDate.valueChanges) {
      if (this.orderForm.controls.fromDate.value) {
        this.minDateTo = new Date(this.orderForm.controls.fromDate.value);
        if (
          !this.orderForm.controls.toDate.value ||
          this.orderForm.controls.toDate.value < this.minDateTo
        ) {
          this.orderForm.controls.toDate.setValue(this.minDateTo);
        }
      } else {
        this.minDateTo = new Date();
        this.orderForm.controls.toDate.setValue('');
      }
    }
  }
}
