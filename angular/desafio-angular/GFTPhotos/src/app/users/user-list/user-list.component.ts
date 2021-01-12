import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/login/authentication.service';
import { DialogComponent } from '../dialog/dialog.component';
import { User } from '../classes/user';
import { UserService } from '../user.service';


@Component({
    selector: 'app-user-list',
    templateUrl: './user-list.component.html'
})
export class UserListComponent implements OnInit, AfterViewInit {

    filter: string = '';
    USER_DATA: User[] = [];
    users = new MatTableDataSource<User>(this.USER_DATA);
    displayedColumns: string[] = ['id', 'name', 'username', 'email', 'details'];

    @ViewChild(MatSort) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;


    constructor(
        private userService: UserService, private authService: AuthenticationService,
        private router: Router, public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        if (!this.authService.isLogged()) {
            this.router.navigate(['login'])
        } else {
            this.listar()
        }
    }

    ngAfterViewInit(): void {
        this.users.sort = this.sort;
        this.users.paginator = this.paginator;
    }

    openDialog(user: User) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '450px';
        dialogConfig.height = '450px';
        dialogConfig.data = {
            name: user.name, username: user.username, email: user.email, street: user.address.street,
            suite: user.address.suite, city: user.address.city, phone: user.phone, website: user.website,
            company: user.company.name, cep: user.address.zipcode, lat: user.address.geo.lat, long: user.address.geo.lng
        };

        this.dialog.open(DialogComponent, dialogConfig);
    }

    listar() {
        this.userService.getUsers()
            .subscribe(data => {
                this.users.data = data as User[];
            });
    }

    filtrar(filtro: string) {
        this.users.filter = filtro.trim().toLocaleLowerCase();
        // query = query.trim().toLowerCase();

        // if (query) {
        //     return users.filter(user => 
        //         user.name.toLowerCase().includes(query) 
        //         || user.email.toLowerCase().includes(query) 
        //         || user.username.toLowerCase().includes(query))
        // } else {
        //     return users;
        // }
    }
}