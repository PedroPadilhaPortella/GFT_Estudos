import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../login/authentication.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

    constructor(private authService: AuthenticationService, private router: Router) { }

    ngOnInit() {
        if (!this.authService.isLogged()) {
            this.router.navigate(['login'])
        }
    }
}
