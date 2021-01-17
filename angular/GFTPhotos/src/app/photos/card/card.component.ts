import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogComponent } from '../dialog/dialog.component';
import { Photo } from '../photo';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html'
})
export class CardComponent {

    @Input() photos: Photo[];
    public paginaAtual = 1;
    filter: string = '';

    constructor(
        public dialog: MatDialog
    ) { }

    openDialog(photo: Photo) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '700px';
        dialogConfig.height = '800px';
        dialogConfig.data = { url: photo.url, description: photo.title };

        this.dialog.open(DialogComponent, dialogConfig);
    }
}