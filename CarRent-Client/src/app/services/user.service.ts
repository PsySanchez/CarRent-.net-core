import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

import { UserModel } from 'src/app/models/index';
import { FormGroup } from '@angular/forms';
import { UserHelper } from '../helpers/user.helper';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  register(regForm: FormGroup) {
    console.log(UserHelper.getFormData(regForm));
    return this.http.post(
      `${environment.apiUrl}/users/registration`,
      UserHelper.getFormData(regForm)
    );
  }
}
