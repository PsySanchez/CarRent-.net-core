import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService } from 'src/app/services';

@Component({
  selector: 'app-add-new-car',
  templateUrl: './add-new-car.component.html',
  styleUrls: ['./add-new-car.component.scss'],
})
export class AddNewCarComponent implements OnInit {
  public uploadForm: FormGroup;
  private image: File;
  constructor(
    private fb: FormBuilder,
    private readonly carService: CarsService
  ) {}

  ngOnInit(): void {
    this.uploadForm = this.fb.group({
      image: [null, Validators.required],
      model: ['', Validators.required],
      manufacturer: ['', Validators.required],
      carNumber: ['', Validators.required],
      pricePerDay: [Number, Validators.required],
    });
  }

  onFileSelected(event: any) {
    if (event.target.files[0].type.match('image/png')) {
      this.uploadForm.controls.image.setValue(event.target.files[0]);
      this.image = event.target.files[0];
    } else {
      console.log('not png');
    }
  }

  onSubmit() {
    const blob = this.image.slice(0, this.image.size, 'image/png');
    this.uploadForm.controls.model.value.charAt(0).toUpperCase();
    const imageName = this.uploadForm.controls.manufacturer.value + this.uploadForm.controls.model.value;
    const newImage = new File([blob], `${imageName}.png`, {type: 'image/png'});
    this.uploadForm.controls.image.setValue(imageName);

    this.carService.addImage(newImage).subscribe(() => {
      console.log('image upload');
    },
    (error) => {
      console.log('image upload error');
    });

    this.carService.addNewCar(this.uploadForm).subscribe(
      () => {
        console.log('car upload');
      },
      (error) => {
        console.log('upload car err');
      }
    );
  }
}
