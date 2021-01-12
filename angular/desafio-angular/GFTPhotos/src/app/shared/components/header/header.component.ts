import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/login/authentication.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html'
})
export class HeaderComponent {

    constructor(private authService: AuthenticationService, private router: Router) {}

    logout() {
        this.authService.logout()
        this.router.navigate(['login'])
    }
}