import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-cust-search',
  templateUrl: './cust-search.component.html',
  styleUrls: ['./cust-search.component.scss'],
})
export class CustSearchComponent implements OnInit {
  public searchForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      firstName: [''],
      lastName: [''],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.minLength(10)]],
    });
  }

  public checkIfValid(): boolean {
    if (this.searchForm.controls.email.valid) {
      this.searchForm.controls.phoneNumber.setErrors({required: false});
      this.searchForm.controls.phoneNumber.markAsUntouched();
      return true;
    }
    if (this.searchForm.controls.phoneNumber.valid) {
      this.searchForm.controls.email.setErrors({required: false});
      this.searchForm.controls.email.markAsUntouched();
      return true;
    }

    return false;
  }
  public onSubmit(): void {
    console.log(this.searchForm);
  }
}
