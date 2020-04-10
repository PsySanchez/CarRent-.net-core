import { FormGroup } from '@angular/forms';

export class UserHelper {
    public static getFormData(form: FormGroup): FormData {
        const result = new FormData();
        const keys = Object.keys(form.value);
        for (const key of keys) {
          result.append(key, form.value[key]);
        }
        return result;
      }
}
