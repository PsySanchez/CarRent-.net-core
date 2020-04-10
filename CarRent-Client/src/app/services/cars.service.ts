import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CacheService } from './cache.service';
import { Observable, of } from 'rxjs';
import { CarModel } from 'src/app/models/car.model';
// import { CarManufacturer } from '../models/carManufacturer';

@Injectable({ providedIn: 'root' })
export class CarsService {
  constructor(private http: HttpClient, private cache: CacheService) {}

  public getAllCars(): Observable<CarModel[]> {
    if (this.cache.cars) {
      return of(this.cache.cars);
    }
    return this.http.get<CarModel[]>(`${environment.apiUrl}/cars`).pipe(
      map((cars: CarModel[]) => {
        this.cache.cars = cars;
        return cars;
      })
    );
  }

  public getOneCars(id: number): Observable<CarModel> {
    if (this.cache.cars) {
      return of(this.cache.cars.find(car => car.id === id));
    }

    return this.http.get<any>(`${environment.apiUrl}/cars/${id}`).pipe(
      map((car) => {
        return car;
      })
    );
  }

  //   public getAllModels(): Promise<any> {
  //     if (this.cache.models) {
  //       return of(this.cache.models).toPromise();
  //     }
  //     return this.http
  //       .get(`${environment.apiUrl}/cars/models`)
  //       .toPromise()
  //       .then((models: CarModel[]) => {
  //         this.cache.models = models;
  //         return models;
  //       });
  //   }

  //   public getAllManufacturers(): Promise<any> {
  //     if (this.cache.manufacturers) {
  //       return of(this.cache.manufacturers).toPromise();
  //     }
  //     return this.http
  //       .get(`${environment.apiUrl}/cars/manufacturers`)
  //       .toPromise()
  //       .then((manufacturers: CarManufacturer[]) => {
  //         this.cache.manufacturers = manufacturers;
  //         return manufacturers;
  //       });
  //   }
}
