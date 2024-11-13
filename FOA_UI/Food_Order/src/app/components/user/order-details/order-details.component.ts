import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Order, Restaurant } from 'src/app/models/models';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent {
  restaurants: Restaurant[] = [];
    constructor(
      @Inject(MAT_DIALOG_DATA) public data: Order,
      private dialogRef: MatDialogRef<OrderDetailsComponent>
    ) {}
  
    onClose() {
      this.dialogRef.close();
    }
  }
  

