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
      firstName: ['', Validators.minLength(3)],
      lastName: ['', Validators.minLength(3)],
      email: ['', Validators.email],
      phoneNumber: ['', Validators.minLength(10)],
    });
  }

  onSubmit() {
    console.log(this.searchForm);
  }
}
