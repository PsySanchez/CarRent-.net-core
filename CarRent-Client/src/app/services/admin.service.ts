import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserHelper } from '../helpers/user.helper';

@Injectable({ providedIn: 'root' })
export class AdminService {
  constructor(private http: HttpClient) {}
  public addNewCar(uploadForm: FormGroup): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/admin/addNewCar`,
      UserHelper.getFormData(uploadForm)
    );
  }

  public addImage(image: File): Observable<any> {
    const imageForm = new FormData();
    imageForm.append('image', image);
    return this.http.post(
      `${environment.apiUrl}/admin/uploadImage`,
      imageForm
    );
  }
}
