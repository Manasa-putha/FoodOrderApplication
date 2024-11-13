import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Route, Router } from '@angular/router';
import { Menu, Restaurant } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { ToasterService } from 'src/app/services/toaster.service';
import { AdmindashboardComponent } from '../admindashboard/admindashboard.component';

@Component({
  selector: 'app-managment',
  templateUrl: './managment.component.html',
  styleUrls: ['./managment.component.css']
})
export class ManagmentComponent {
      restaurants: Restaurant[] = [];
      newMenuItem = { dishName: '', price: 0, description: '' };
      constructor(private apiService: AuthService, private dialog: MatDialog,  private snackBar: MatSnackBar,private toster:ToasterService) {
        this.loadRestaurants();
      }
      restaurant = {
        restaurantId: null as number | null,  
        restaurantName: '',
        location: '',
        cuisine: '',
        ownerId: null, 
        user: null,
        orders: [],
        reviews: [],
        menus: [] as Menu[]  
      };
    
      loadRestaurants() {
        this.apiService.getRestaurants().subscribe((data: Restaurant[]) => {
          this.restaurants = data;
        });
      }
    
      addRestaurant() {
        const userId = this.apiService.getCurrentUserId(); 
        const dialogRef = this.dialog.open(AdmindashboardComponent, {
          width: '1000px',
          height:'800px',
          data: { ownerId: userId }
        });
        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            result.ownerId = userId;
            this.apiService.registerRestaurant(result).subscribe(() => {
              this.loadRestaurants(); 
            });
          }
        });
      }
    
      editRestaurant(restaurant: Restaurant) {
        const dialogRef = this.dialog.open(AdmindashboardComponent, {
          width: '1000px',
          height: '600px',
          data: restaurant,
        });
        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.apiService.updateRestaurant(result).subscribe(() => {
              this.loadRestaurants(); 
            });
          }
        });
      }
    
      deleteRestaurant(restaurant: Restaurant) {
        this.apiService.deleteRestaurant(restaurant.restaurantId).subscribe(() => {
          this.loadRestaurants(); 
        });
      }
      
    // Add a new menu item
    addMenuItem(restaurant: Restaurant) {
      if (this.restaurant.restaurantId) {
        this.apiService.addMenuItem(restaurant.restaurantId, this.newMenuItem).subscribe(response => {
          const newItem = response.updatedItem; 
          this.restaurant.menus.push(newItem); 
          this.newMenuItem = { dishName: '', price: 0, description: '' };  
          // this.snackBar.open('Menu item added successfully', 'Close', { duration: 2000 });
          this.toster.showSuccess('Menu item added successfully', 'Close');
        }, error => {
          console.error('Error adding menu item', error);
          // this.snackBar.open('Error adding menu item', 'Close', { duration: 2000 });
          this.toster.showError('Error adding menu item', 'Close');
        });
      } else {
        this.snackBar.open('Restaurant not saved. Cannot add menu item.', 'Close', { duration: 2000 });
      }
    }


  cancelMenuItem() {
    this.resetMenuItem();
  }

  resetMenuItem() {
    this.newMenuItem.dishName = '';
    this.newMenuItem.price = 0;
    this.newMenuItem.description = '';
  }
   
deleteMenuItem(restaurant: Restaurant, item: Menu) {
  if (restaurant.restaurantId && item.menuId) {
    this.apiService.deleteMenuItem(restaurant.restaurantId, item.menuId).subscribe(
      () => {
        this.restaurant.menus = this.restaurant.menus.filter(menuItem => menuItem.menuId !== item.menuId);
        // this.snackBar.open('Menu item deleted successfully', 'Close', { duration: 2000 });
        this.toster.showSuccess('Menu item deleted successfully', 'Close');
        this.loadRestaurants();
      },
      error => {
        console.error('Error deleting menu item', error);
        // this.snackBar.open('Error deleting menu item', 'Close', { duration: 2000 });
        this.toster.showError('Error deleting menu item', 'Close');
      }
    );
  }
}

cancelEdit(menu: Menu) {
  menu.isEditing = false; 
}

    editMenuItem(item: Menu) {
      item.isEditing = true; 
    }

  saveMenuItem(restaurant: Restaurant, item: Menu) {
    this.apiService.updateMenuItem(restaurant.restaurantId, item.menuId, item).subscribe(() => {
      item.isEditing = false; 
      // this.snackBar.open('Menu item updated successfully', 'Close', { duration: 2000 });
      this.toster.showSuccess('Menu item updated successfully', 'Close');
    }, error => {
      console.error('Error updating menu item', error);
      // this.snackBar.open('Error updating menu item', 'Close', { duration: 2000 });
      this.toster.showError('Error updating menu item', 'Close',);
    });
  }
  
  }

    
  

