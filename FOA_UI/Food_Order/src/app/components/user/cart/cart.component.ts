import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { forkJoin, map } from 'rxjs';
import { Cart, CartItem, Menu, Order, OrderStatus, Restaurant, User } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { ToasterService } from 'src/app/services/toaster.service';

// Extending CartItem with Menu properties
interface CartItemExtended extends CartItem {
  menu: Menu; // Reference to Menu details (dishName, price, etc.)
  selected: boolean;
}


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit{
 
  cartItems: CartItemExtended[] = []; 
  userId!: number; 
  loading: boolean = false;

  constructor(private apiService: AuthService, private snackBar: MatSnackBar,private router:Router,private toaster:ToasterService) {}

  ngOnInit() {
    this.userId = this.apiService.getCurrentUserId();
    this.loadCart();
}
// loadCart() {
//   this.loading = true; 
//   this.apiService.getCart(this.userId).subscribe({
//     next: (response: Cart) => {
//       this.cartItems = response.items || [];
//       this.loading = false; 
//     },
//     error: (err) => {
//       this.snackBar.open('Error loading cart', 'Close', { duration: 2000 });
//       this.loading = false; 
//     }
//   });
// }


loadCart() {
  this.loading = true;

  this.apiService.getCart(this.userId).subscribe({
    next: (response) => {
      if (response?.items) {
        // Fetch menu details for each cart item using menuId
        const menuRequests = response.items.map((item: CartItem) =>
          this.apiService.getMenuById(item.menuId).pipe(
            map((menu) => ({
              ...item,
              menu: menu, // Attach the fetched menu to the CartItem
              selected: false
            }))
          )
        );

        forkJoin(menuRequests).subscribe({
          next: (cartItemsExtended) => {
            this.cartItems = cartItemsExtended;
            this.loading = false;
          },
          error: () => {
            this.snackBar.open('Error loading cart items', 'Close', { duration: 2000 });
            this.toaster.showInfo('Error loading cart items', 'Close'),{ duration: 2000 };
            this.loading = false;
          }
        });
      }
    },
    error: () => {
      this.snackBar.open('Error loading cart', 'Close', { duration: 2000 });
      this.toaster.showInfo('Error loading cart ', 'Close'),{ duration: 2000 };
      this.loading = false;
    }
  });
}


calculateSelectedTotal(): number {
  return this.cartItems
    .filter(item => item.selected)
    .reduce((sum, item) => sum + (item.menu.price * item.quantity), 0); // Ensure we're using 'menu.price'
}

toggleSelectAll(checked: boolean) {
  this.cartItems.forEach(item => item.selected = checked);
}

allSelected(): boolean {
  return this.cartItems.every(item => item.selected);
}

hasSelectedItems(): boolean {
  return this.cartItems.some(item => item.selected);
}

 removeFromCart(dish: Menu) {
  this.cartItems = this.cartItems.filter(item => item.menuId !== dish.menuId);
  this.apiService.updateCartItems(this.cartItems).subscribe({
    next: () => this.snackBar.open(`${dish.dishName} removed from cart`, 'Close', { duration: 2000 }),
    error: () => this.snackBar.open('Error removing item from cart', 'Close', { duration: 2000 })
  });
}

// Place order

placeOrder() {
  const selectedItems = this.cartItems.filter(item => item.selected);

  if (selectedItems.length === 0) {
    this.snackBar.open('Please select items to place an order', 'Close', { duration: 2000 });
    this.toaster.showInfo('Please select items to place an order', 'Close'),{ duration: 2000 };
    return;
  }

  const restaurantId = selectedItems[0]?.menu.restaurantId;
  const newOrder: Order = {
    orderId: 0,
    userId: this.userId,
    restaurantId: restaurantId,
    orderDate: new Date(),
    orderStatus: OrderStatus.Placed,
    items: selectedItems,
    user: { id: this.userId } as User,
    restaurant: { restaurantId } as Restaurant
  };

  this.apiService.placeOrder(newOrder).subscribe({
    next: (createdOrder) => {
      this.snackBar.open('Order placed successfully!', 'Close', { duration: 2000 });
      this.toaster.showSuccess('Order placed successfully!', 'Close'),{ duration: 2000 };
      this.cartItems = [];
      this.apiService.updateCartItems([]).subscribe();
     
      this.router.navigateByUrl('/list');
    },
    error: () => {
      this.snackBar.open('Error placing order', 'Close', { duration: 2000 });
      this.toaster.showError('Error placing order', 'Close'),{ duration: 2000 };
    }
  });
}
}