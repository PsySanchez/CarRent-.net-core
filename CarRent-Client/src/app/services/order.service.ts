import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { UserHelper } from '../helpers/user.helper';

@Injectable({ providedIn: 'root' })
export class OrderService {
  constructor(private http: HttpClient) {}

  addNewOrder(orderForm: FormGroup): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/orders`,
      UserHelper.getFormData(orderForm)
    );
  }

  getUserOrders(): Promise<any> {
    return this.http
      .get<any>(`${environment.apiUrl}/userOrders`)
      .toPromise()
      .then((data) => {
        return data;
      });
  }
}
