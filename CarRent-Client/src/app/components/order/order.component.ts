import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarsService } from 'src/app/services';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  constructor( private route: ActivatedRoute, private carsService: CarsService ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.carsService.getOneCars(Number(params.carId)).subscribe(car => {
          console.log(car);
      });
    });

  }
}
