import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-restaurent',
  templateUrl: './restaurent.component.html',
  styleUrls: ['./restaurent.component.css']
})
export class RestaurentComponent {
  registerRestaurant: FormGroup;
  newMenuItem: FormGroup;

  constructor(
    private fb: FormBuilder,
    private apiService: AuthService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.registerRestaurant = this.fb.group({
      restaurantName: ['', Validators.required],
      restaurantAddress: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
    });
    this.newMenuItem = this.fb.group({
      itemName: ['', Validators.required],
      price: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  registerNewRestaurant() {
    const restaurantData = {
      name: this.registerRestaurant.get('restaurantName')?.value,
      address: this.registerRestaurant.get('restaurantAddress')?.value,
      phone: this.registerRestaurant.get('phoneNumber')?.value,
    };

    this.apiService.registerRestaurant(restaurantData).subscribe({
      next: (res) => this.snackBar.open('Restaurant Registered Successfully', 'OK'),
      error: (err) => this.snackBar.open('Error Registering Restaurant', 'OK'),
    });
  }

  addNewMenuItem() {
  }

}
