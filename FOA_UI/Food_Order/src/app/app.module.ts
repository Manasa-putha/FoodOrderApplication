import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PageFooterComponent } from './components/dashboard/page-footer/page-footer.component';
import { PageHeaderComponent } from './components/dashboard/page-header/page-header.component';
import { PageNotFoundComponent } from './components/dashboard/page-not-found/page-not-found.component';
import { PageSideNavComponent } from './components/dashboard/page-side-nav/page-side-nav.component';
import { PageTableComponent } from './components/dashboard/page-table/page-table.component';
import { LoginComponent } from './components/auth/login/login.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { ToastrModule } from 'ngx-toastr';
import { RestaurantListComponent } from './components/user/restaurant-list/restaurant-list.component';
import { OrderTrakingComponent } from './components/user/order-traking/order-traking.component';
import { ManagmentComponent } from './components/admin/managment/managment.component';
import { RestaurentComponent } from './components/admin/restaurent/restaurent.component';
import { AdmindashboardComponent } from './components/admin/admindashboard/admindashboard.component';
import { DishDetailsDialogComponent } from './components/user/dish-details-dialog/dish-details-dialog.component';
import { CartComponent } from './components/user/cart/cart.component';
import { OrderDetailsComponent } from './components/user/order-details/order-details.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    PageFooterComponent,
    PageHeaderComponent,
    PageNotFoundComponent,
    PageSideNavComponent,
    PageTableComponent,
    LoginComponent,
    SignupComponent,
    RestaurantListComponent,
    OrderTrakingComponent,
    ManagmentComponent,
    RestaurentComponent,
    AdmindashboardComponent,
    DishDetailsDialogComponent,
    CartComponent,
    OrderDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatListModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    MatDialogModule,
    ToastrModule.forRoot({
      timeOut: 3000,  // Configure global Toastr settings
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
