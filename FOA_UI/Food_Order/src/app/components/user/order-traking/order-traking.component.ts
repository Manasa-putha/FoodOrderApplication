import { Component, OnInit } from '@angular/core';
import { Order, OrderStatus } from 'src/app/models/models';
import { AuthService } from 'src/app/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { OrderDetailsComponent } from '../order-details/order-details.component';
@Component({
  selector: 'app-order-traking',
  templateUrl: './order-traking.component.html',
  styleUrls: ['./order-traking.component.css']
})
export class OrderTrakingComponent implements OnInit{
  
  orders: Order[] = [];
  isAdmin: boolean = false;
  selectedOrder: Order | null = null;
  filteredOrders: Order[] = [];
  availableStatuses = [
    { label: 'Placed', value: OrderStatus.Placed },
    { label: 'Cancelled', value: OrderStatus.Cancelled },
    { label: 'Preparing', value: OrderStatus.Preparing },
    { label: 'Ready', value: OrderStatus.Ready },
    { label: 'Delivered', value: OrderStatus.Delivered }
  ];
  loading: boolean = false; 
  displayedColumns: string[] = ['userId', 'orderStatus', 'orderDate', 'actions']; // Define displayed columns
  constructor(private apiService: AuthService, private authService: AuthService) {
  
  }
  ngOnInit() {
    this.checkUserRole();
    this.loadOrders();
  }

  private checkUserRole() {
    const role = this.authService.getRoleFromToken(); 
    this.isAdmin = role === 'Admin'; 
  }
  private loadOrders() {
    if (this.isAdmin) {
      // Load orders if admin
      this.apiService.getOrders().subscribe((data: Order[]) => {
        this.orders = data; 
        this.filteredOrders = this.orders; 
      }, error => {
        console.error('Error loading orders:', error);
      });
    } else {
      const userId: number = this.authService.getCurrentUserId(); 
      // Load orders specific to the user
      this.apiService.getOrdersByUserId(userId.toString()).subscribe((data: Order[]) => {
        this.orders = data; 
        this.filteredOrders = this.orders; 
      }, error => {
        console.error('Error loading user orders:', error);
      });
    }
  }

  updateOrderStatus(order: Order, OrderStatus: number) {
    if (this.isAdmin) {
      console.log('Updating order:', order); 
      console.log('Order ID:', order.orderId); 
      this.apiService.updateOrderStatus(order.orderId, OrderStatus).subscribe((response) => {
        console.log('Order status updated successfully:', response);
        this.loadOrders();
      }, error => {
        console.error('Error updating order status:', error);
      });
    }
  }

// filterOrdersByStatus(status: string) {
//   if (this.isAdmin) {
   
//     const statusEnumValue = OrderStatus[status as keyof typeof OrderStatus];
//     this.filteredOrders = this.orders.filter(order => order.orderStatus === statusEnumValue);
//   }
// }
filterOrdersByStatus(status: OrderStatus) {
  if (this.isAdmin) {
    this.filteredOrders = this.orders.filter(order => order.orderStatus === status);
  }
}


  cancelOrder(order: Order) {
    if (this.isAdmin && confirm('Are you sure you want to cancel this order?')) {
      this.updateOrderStatus(order, 1);
    }
  }
 
  selectOrder(order: Order) {
    this.selectedOrder = order;
  }
  // View items within an order
  viewOrderItems(order: Order) {
    this.selectOrder(order);
    console.log('Items in order:', order.items);
  }

}
