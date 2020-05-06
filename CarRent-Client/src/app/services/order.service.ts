import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { UserHelper } from '../helpers/user.helper';
import { CacheService } from './cache.service';
import { OrderModel } from '../models/order.model';
import { map } from 'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class OrderService {
  constructor(private http: HttpClient, private cache: CacheService) {}

  addNewOrder(orderForm: FormGroup): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/orders`,
      UserHelper.getFormData(orderForm)
    );
  }

  getUserOrdersHistory(userEmail: string): Observable<any> {
    return this.http
      .get<any>(`${environment.apiUrl}/orders/userOrdersHistory/${userEmail}`).pipe(
        map((orders: OrderModel[]) => {
          this.cache.userOrders = orders;
          return orders;
        })
      );
  }
}
