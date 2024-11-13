export interface User {
  id: number;
  userName: string;
  email: string;
  mobileNumber: string;
  alternativeNumber:string;
  password: string;
  // userType: UserType;
  role:Role;
  pincode:number;
  city:string;
  accountStatus: AccountStatus;
  createdAt: string;
  updatedAt: string;
  tokensAvailable:number;
  basePrice:number;
  orders?: Order[];  
  restaurantsOwned?: Restaurant[];  
  reviews?: Review[]; 
}

export enum AccountStatus {
  UNAPROOVED,
  ACTIVE,
  BLOCKED,
}

export enum Role {
 
  None,Admin,Customer
}
export interface Restaurant {
  restaurantId: number;
  restaurantName: string;
  location: string;
  cuisine: string;
  ownerId: number;
  user: User;  
  menus: Menu[];
  orders: Order[];
  reviews: Review[];
}
export interface Menu {
  menuId: number;
  restaurantId: number;
  dishName: string;
  price: number;
  description: string;
  quantity?: number; 
  cartItems?:CartItem[];
  isEditing?: boolean;
}
export enum OrderStatus {
  Placed = 0,
  Cancelled = 1,
  Preparing = 2,
  Ready = 3,
  Delivered = 4,
}

export interface Order {
  orderId: number;
  userId: number;
  user: User;
  restaurantId: number;
  restaurant: Restaurant;
  orderDate: Date;
  orderStatus: OrderStatus;
  items: CartItem[];
}
export interface Review {
  reviewId: number;
  userId: number;
  user: User;
  restaurantId: number;
  restaurant: Restaurant;
  dishId: number;
  dish: Menu;
  rating: number;
  reviewText?: string;
}

export enum UserType {
  None = 'None',
  Customer = 'Customer',
  RestaurantOwner = 'RestaurantOwner',
  Admin = 'Admin'
}
export interface Cart {
  cartId: number;
  userId: number;
  items?: CartItem[];
}
export interface CartItem {
  // restaurantId?: any;
  cartItemId: number;
  cartId: number;
  menuId: number;
  quantity: number;
  // dishName?: string; 
  // price?: number; 
  // restaurantId?:number;
}




