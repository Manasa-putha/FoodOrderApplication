  import { Component, Inject } from '@angular/core';
  import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
  import { MatSnackBar } from '@angular/material/snack-bar';
  import { Menu } from 'src/app/models/models';
  import { AuthService } from 'src/app/services/auth.service';
import { ToasterService } from 'src/app/services/toaster.service';
  
  @Component({
    selector: 'app-admindashboard',
    templateUrl: './admindashboard.component.html',
    styleUrls: ['./admindashboard.component.css']
  })
  export class AdmindashboardComponent {
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
  
    newMenuItem = { dishName: '', price: 0, description: '' };  
  
    constructor(
      @Inject(MAT_DIALOG_DATA) public data: any,
      private dialogRef: MatDialogRef<AdmindashboardComponent>,
      private apiservice: AuthService,
      private snackBar: MatSnackBar,
      private toaster:ToasterService
    ) {
      if (data) {
        this.restaurant = { ...data };  
      }
    }
    closeDialog(): void {
      this.dialogRef.close();
    }
  saveRestaurant() {
      this.dialogRef.close(this.restaurant); 
    }
    saveMenuItem(item: Menu) {
      if (this.restaurant.restaurantId && item.menuId) {
        this.apiservice.updateMenuItem(this.restaurant.restaurantId, item.menuId, item).subscribe(response => {
          item.isEditing = false; // Exit edit mode
          //this.snackBar.open('Menu item updated successfully', 'Close', { duration: 2000 });
          this.toaster.showSuccess('Menu item updated successfully', 'Close');
        }, error => {
          console.error('Error updating menu item', error);
          this.snackBar.open('Error updating menu item', 'Close', { duration: 2000 });
          this.toaster.showError('Error updating menu item', 'Close');
        });
      }
    }
  
  editMenuItem(item: Menu) {
    item.isEditing = true; 
  }
  cancelEdit(item: Menu) {
    item.isEditing = false; 
  }
    // Add a new menu item
    addMenuItem() {
      if (this.restaurant.restaurantId) {
        this.apiservice.addMenuItem(this.restaurant.restaurantId, this.newMenuItem).subscribe(response => {
          const newItem = response.updatedItem; 
          this.restaurant.menus.push(newItem); 
         // this.restaurant.menus.push(response);  // Add the menu item to the list
          this.newMenuItem = { dishName: '', price: 0, description: '' };  // Clear form
          // this.snackBar.open('Menu item added successfully', 'Close', { duration: 2000 });
          this.toaster.showSuccess('Menu item added successfully', 'Close');
        }, error => {
          console.error('Error adding menu item', error);
          // this.snackBar.open('Error adding menu item', 'Close', { duration: 2000 });
          this.toaster.showError('Error adding menu item', 'Close');
        });
      } else {
        this.snackBar.open('Restaurant not saved. Cannot add menu item.', 'Close', { duration: 2000 });
      }
    }
  
    // Delete a menu item
    deleteMenuItem(item: Menu) {
      if (this.restaurant.restaurantId && item.menuId) {
        this.apiservice.deleteMenuItem(this.restaurant.restaurantId, item.menuId).subscribe(() => {
          this.restaurant.menus = this.restaurant.menus.filter(menuItem => menuItem.menuId !== item.menuId);  // Remove deleted item
          // this.snackBar.open('Menu item deleted successfully', 'Close', { duration: 2000 });
          this.toaster.showSuccess('Menu item deleted successfully', 'Close');
        }, error => {
          console.error('Error deleting menu item', error);
          // this.snackBar.open('Error deleting menu item', 'Close', { duration: 2000 });
          this.toaster.showError('Error deleting menu item', 'Close');
        });
      }
    }
    
  }
  