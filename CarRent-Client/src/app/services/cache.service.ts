import { CarModel, UserModel } from '../models';
import { Injectable } from '@angular/core';
import { HttpRequest, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CacheService {
  cars: CarModel[] = null;
  user: UserModel[] = null;
  lastRequest: HttpRequest<any>;

  constructor(private http: HttpClient) {}

  public retryLastRequest(): Observable<any> {
    console.log(this.lastRequest);
    return this.http.request(this.lastRequest).pipe();
  }
}
