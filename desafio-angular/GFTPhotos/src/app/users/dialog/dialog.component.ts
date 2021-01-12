import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-dialog',
    templateUrl: './dialog.component.html'
})
export class DialogComponent {

    constructor(
        private dialogRef: MatDialogRef<DialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: 
        {
            name: string, username: string, email: string, street: string, 
            suite: string, city: string, phone: string, website: string, company: string,
            cep: string, lat: string, long: string
        }
    ) {}

    close() {
        this.dialogRef.close();
    }
}
