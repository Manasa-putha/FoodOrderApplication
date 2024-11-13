import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Menu } from 'src/app/models/models';

@Component({
  selector: 'app-dish-details-dialog',
  templateUrl: './dish-details-dialog.component.html',
  styleUrls: ['./dish-details-dialog.component.css']
})
export class DishDetailsDialogComponent {

    constructor(
      @Inject(MAT_DIALOG_DATA) public data: Menu,
      private dialogRef: MatDialogRef<DishDetailsDialogComponent>
    ) {}
  
    onClose() {
      this.dialogRef.close();
    }
  }
  

