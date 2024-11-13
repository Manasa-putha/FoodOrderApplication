import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';
import { Cart, CartItem, Menu, Order, OrderStatus, Restaurant, Review } from '../models/models';
// import { Bill, User, UserDashboard } from '../models/models';
import { TokenApiModel } from '../models/token-api.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private cartItems: Menu[] = [];
  private baseUrl: string = 'https://localhost:7094/api/Auth/';
  private baseUrl1: string = 'https://localhost:7094/api/Orders/';
  private baseUrl2: string = 'https://localhost:7094/api/Admin/';
   userStatus: Subject<string> = new Subject();
 // userStatus: BehaviorSubject<string> = new BehaviorSubject<string>(this.getUserStatus());
  private userPayload:any;
  private jwtHelper = new JwtHelperService();
  constructor(
    // private jwt: JwtHelperService,
    private http:HttpClient,
     private router: Router) { 
      this.userPayload = this.decodedToken();
  }
  private getUserStatus(): string {
    return localStorage.getItem('userStatus') || 'loggedOff';
  }
  signUp(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}register`,userobj)
  }
  login(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}login`,userobj)
  }
  logOut() {
    // localStorage.removeItem('access_token');
    // // this.userInfo = null;
    // this.userStatus.next('loggedOff');
    // this.userStateService.updateTokens(0);
    this.userStatus.next('loggedOff');
    localStorage.clear();
    this.userStatus.next('loggedOff');
    this.router.navigate(['login'])
  }

  // signOut(){
  //   localStorage.clear();
  //   this.userStatus.next('loggedOff');
  //   this.router.navigate(['login'])
  // }
  

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }
  storeRefreshToken(tokenValue: string){
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }
  getRefreshToken(){
    return localStorage.getItem('refreshToken')
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token) {
      // Check if the token is expired
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }

  getfullNameFromToken(){
    if(this.userPayload)
    return this.userPayload.name;
  }

  getRoleFromToken(){
    if(this.userPayload)
    return this.userPayload.role;
  }

  renewToken(tokenApi : TokenApiModel){
    return this.http.post<any>(`${this.baseUrl}refresh`, tokenApi)
  }


getCurrentUserId(): number {
  // Implement your logic to retrieve the current user ID, possibly from the token or session
  const tokenPayload = this.decodedToken();
   return tokenPayload.nameid;
//   if (tokenPayload && tokenPayload.nameid) {
//     return parseInt(tokenPayload.nameid, 10); // Adjust this to match the claim name for userId
//   }
//   return 0;
 }
 // Add getRestaurants method
 getRestaurants(): Observable<Restaurant[]> {
  return this.http.get<Restaurant[]>(`${this.baseUrl1}restaurants`);
}
  
   getOrders(): Observable<any> {
     return this.http.get(`${this.baseUrl1}orders`);
   }
 
   respondToReview(reviewId: number, response: string): Observable<any> {
     return this.http.post(`${this.baseUrl1}/reviews/${reviewId}/response`, { response });
   }

  registerRestaurant(restaurantData: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    return this.http.post(`${this.baseUrl2}registerRestaurant`, restaurantData, { headers });
  }
addMenuItem(restaurantId: number, menuItem: any): Observable<any> {
  return this.http.post(`${this.baseUrl2}${restaurantId}/menu`, menuItem);
}

// Update a menu item
updateMenuItem(restaurantId: number, menuId: number, menuItem: any): Observable<any> {
  return this.http.put(`${this.baseUrl2}${restaurantId}/editMenuItem/${menuId}`, menuItem);
}

// Delete a menu item
deleteMenuItem(restaurantId: number, menuId: number): Observable<any> {
  return this.http.delete(`${this.baseUrl2}${restaurantId}/deleteMenuItem/${menuId}`);
}


  processOrder(orderId: number, newStatus: OrderStatus): Observable<any> {
    return this.http.put(`${this.baseUrl2}processOrder/${orderId}`, { newStatus });
  }
  
    manageUserAccount(userId: number): Observable<any> {
      return this.http.get(`${this.baseUrl2}user/manageAccount/${userId}`);
    }
  

    manageRestaurants(): Observable<any> {
      return this.http.get(`${this.baseUrl2}restaurants/manage`);
    }
  
    resolveDispute(disputeId: number): Observable<any> {
      return this.http.get(`${this.baseUrl2}disputes/resolve/${disputeId}`);
    }
    getOrderDetails(orderId: number): Observable<Order> {
      return this.http.get<Order>(`${this.baseUrl}/${orderId}`);
    }

    addToCart(userId: number, item: CartItem): Observable<Cart> {
      return this.http.post<Cart>(`${this.baseUrl1}cart/add`, { userId, item });
    }
    
    getCart(userId: number): Observable<Cart> {
      return this.http.get<Cart>(`${this.baseUrl1}cart/${userId}`);
    }
    updateCart(userId: number, items: CartItem[]): Observable<Cart> {
      return this.http.put<Cart>(`${this.baseUrl1}cart/${userId}/update`, items);
    }
    
    clearCart(userId: number): Observable<any> {
      return this.http.delete(`${this.baseUrl1}cart/${userId}/clear`);
    }
  
    // Fix the method to accept CartItem[] and return an Observable
    updateCartItems(cartItems: CartItem[]): Observable<any> {
      return this.http.put(`${this.baseUrl1}cart/update`, cartItems);
    }

placeOrder(order: Order): Observable<any> {
  return this.http.post(`${this.baseUrl1}orders`, order);
}
  // View customer reviews for a restaurant
  getRestaurantReviews(restaurantId: number): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.baseUrl1}restaurantReviews/${restaurantId}`);
  }
  // Respond to a customer review
  // respondToReview(reviewId: number, response: string): Observable<any> {
  //   return this.http.post(`${this.baseUrl1}respondReview/${reviewId}`, { response });
  
  // }
    // Fetch orders specific to a user by user ID
    getOrdersByUserId(userId: string): Observable<any[]> {
      return this.http.get<any[]>(`${this.baseUrl1}userOrders/${userId}`); 
    }
  
    // Update the status of an order
    updateOrderStatus(orderId: number, OrderStatus: number): Observable<any> {
      return this.http.put<any>(`${this.baseUrl1}updateStatus/${orderId}`, { OrderStatus }); 
    }
    // Delete a restaurant by its ID
deleteRestaurant(restaurantId: number): Observable<any> {
  return this.http.delete(`${this.baseUrl2}deleteRestaurant/${restaurantId}`);
}
updateRestaurant(restaurant: Restaurant): Observable<any> {
  return this.http.put(`${this.baseUrl2}updateRestaurant/${restaurant.restaurantId}`, restaurant);
}

  // Method to get a menu by its ID
  getMenuById(menuId: number): Observable<Menu> {
    return this.http.get<Menu>(`${this.baseUrl2}menu/${menuId}`);
  }
}
 
