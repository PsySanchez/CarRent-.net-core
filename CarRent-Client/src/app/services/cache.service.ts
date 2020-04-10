import { CarModel, UserModel } from '../models';
import { Injectable } from '@angular/core';

@Injectable()
export class CacheService{
    cars: CarModel[] = null;
    user: UserModel[] = null;
}
