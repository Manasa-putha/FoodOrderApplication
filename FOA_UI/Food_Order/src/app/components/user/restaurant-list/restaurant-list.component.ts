import { Component, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Cart, CartItem, Menu, Restaurant } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { ToasterService } from 'src/app/services/toaster.service';
import { DishDetailsDialogComponent } from '../dish-details-dialog/dish-details-dialog.component';
@Component({
  selector: 'app-restaurant-list',
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css']
})

export class RestaurantListComponent implements OnInit{
  displayedColumns: string[] = ['dishName', 'price', 'addToCart','dishImage'];
  restaurants: Restaurant[] = [];
  restaurantsToDisplay: Restaurant[] = [];
   cartItems: Menu[] = [];
  cart!: CartItem[];
  userId!:number;
  constructor(private apiService: AuthService, private snackBar: MatSnackBar, public dialog: MatDialog,public router:Router,public toaster:ToasterService) {
    apiService.getRestaurants().subscribe({
      next: (res: Restaurant[]) => {
        this.restaurants = res;
        this.updateList();
      },
      error: () => {
        //this.snackBar.open('Failed to load restaurants', 'Close', { duration: 2000 });
        this.toaster.showError('Failed to load restaurants', 'Close');
      }
    });
  }

  ngOnInit(): void {
    this.userId = this.apiService.getCurrentUserId();
    if (this.userId === undefined || this.userId === null) {
        //this.snackBar.open('User ID is not available', 'Close', { duration: 2000 });
        this.toaster.showError('User ID is not available', 'Close');
        console.error('User ID is undefined or null');
        return; 
    }
    this.loadCartItems();
  //  this.userId=this.apiService.getCurrentUserId();
    console.log(this.userId)
  }
  loadCartItems() {
  
      this.apiService.getCart(this.userId).subscribe({
        
      next: (cart:Cart) => {
        this.cart = cart.items || []; 
        console.log(this.userId)
        console.log(this.cart);
      },
      error: () => {
       // this.snackBar.open('Failed to load cart items', 'Close', { duration: 2000 });
        this.toaster.showError('Failed to load cart items', 'Close');
      }
    });
  }

  updateList() {
    this.restaurantsToDisplay = [...this.restaurants];
  }

  searchRestaurants(value: string) {
    this.updateList();
    value = value.toLowerCase();
    this.restaurantsToDisplay = this.restaurants.filter((restaurant) => {
      const nameMatch = restaurant.restaurantName.toLowerCase().includes(value);
      const locationMatch = restaurant.location.toLowerCase().includes(value);
      const cuisineMatch = restaurant.cuisine.toLowerCase().includes(value);
      return nameMatch || locationMatch || cuisineMatch;
    });
  }

  getRestaurantCount() {
    return this.restaurantsToDisplay.length;
  }

  viewDishDetails(dish: Menu) {
    const dialogRef = this.dialog.open(DishDetailsDialogComponent, {
      width: '300px',
      data: dish
    });
  }

  addToCart(dish: Menu) {
    const quantity = dish.quantity || 1; 
    const item: CartItem = {
      cartItemId: 0, 
      cartId: 0, 
      menuId: dish.menuId,
      quantity: quantity
    };

    this.apiService.addToCart(this.userId, item) 
      .subscribe({
        next: (cart) => {
          if (cart) {
            this.cartItems.push(dish); 
           // this.snackBar.open(`${dish.dishName} added to cart`, 'Close', { duration: 2000 });
            this.toaster.showSuccess(`${dish.dishName} added to cart`, 'Close');
          } else {
            // this.snackBar.open(`Failed to add ${dish.dishName} to cart`, 'Close', { duration: 2000 });
            this.toaster.showError(`Failed to add ${dish.dishName} to cart`, 'Close');
          }
          this.router.navigateByUrl('/cart');
        },
   
        error: () => {
         // this.snackBar.open(`Error adding ${dish.dishName} to cart`, 'Close', { duration: 2000 });
          this.toaster.showError(`Error adding ${dish.dishName} to cart`, 'Close');
        }
      });
  }
  
  isInCart(dish: Menu): boolean {
    return this.cartItems.some(item => item.menuId === dish.menuId);
  }
}

