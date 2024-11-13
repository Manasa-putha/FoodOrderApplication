import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmindashboardComponent } from './components/admin/admindashboard/admindashboard.component';
import { ManagmentComponent } from './components/admin/managment/managment.component';
import { RestaurentComponent } from './components/admin/restaurent/restaurent.component';
import { LoginComponent } from './components/auth/login/login.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { PageNotFoundComponent } from './components/dashboard/page-not-found/page-not-found.component';
import { CartComponent } from './components/user/cart/cart.component';
import { OrderDetailsComponent } from './components/user/order-details/order-details.component';
import { OrderTrakingComponent } from './components/user/order-traking/order-traking.component';
import { RestaurantListComponent } from './components/user/restaurant-list/restaurant-list.component';
import { adminGuard } from './guards/admin.guard';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'list',component:RestaurantListComponent,canActivate: [authGuard]},
  {path:'track',component:OrderTrakingComponent,canActivate: [authGuard]},
 {path:'admin',component:AdmindashboardComponent,canActivate: [authGuard,adminGuard]},
 {path:'manage',component:ManagmentComponent,canActivate: [authGuard]},
 {path:'res',component:RestaurentComponent,canActivate: [authGuard,adminGuard]},
 {path:'cart',component:CartComponent,canActivate: [authGuard]},
 {path:'details',component:OrderDetailsComponent,canActivate: [authGuard,adminGuard]},
//  { path: 'order-details/:id', component: OrderDetailsComponent,canActivate: [authGuard] },
  // {path:'home',component:HomepageComponent,canActivate: [authGuard,adminGuard]},

  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component:PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
