import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthenticationService } from 'src/app/login/authentication.service';
import { DialogComponent } from '../dialog/dialog.component';
import { Photo } from '../photo';
import { PhotoService } from '../photo.service';

@Component({
    selector: 'app-photo-list',
    templateUrl: './photo-list.component.html'
})
export class PhotoListComponent implements OnInit {

    public photos: Photo[];
    public paginaAtual = 1;
    filter: string = '';

    constructor(
        private photoService: PhotoService,
        private authService: AuthenticationService,
        private router: Router,
        // public dialog: MatDialog,
        private spinner: NgxSpinnerService
    ) { }

    ngOnInit(): void {
        this.spinner.show();
        setTimeout(() => {
            this.spinner.hide();
        }, 1000);

        if (!this.authService.isLogged()) {
            this.router.navigate(['login'])
        } else {
            this.getPhotos()
        }
    }

    getPhotos() {
        this.photoService.getPhotos()
            .subscribe(
                photos => this.photos = photos,
                error => console.log(error)
            );
    }

    // openDialog(photo: Photo) {
    //     const dialogConfig = new MatDialogConfig();
    //     dialogConfig.disableClose = false;
    //     dialogConfig.autoFocus = true;
    //     dialogConfig.width = '700px';
    //     dialogConfig.height = '800px';
    //     dialogConfig.data = { url: photo.url, description: photo.title };

    //     this.dialog.open(DialogComponent, dialogConfig);
    // }
}