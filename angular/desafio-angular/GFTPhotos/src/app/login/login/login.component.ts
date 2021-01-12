import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [
    ]
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;

    constructor(private formBuilder: FormBuilder, private authService: AuthenticationService, private router: Router) { }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            username: ['', [Validators.required, Validators.minLength]],
            email: ['', Validators.required]
        });

    }
    
    login() {
        const username = this.loginForm.get('username').value;
        const email = this.loginForm.get('email').value;

        this.authService
            .authenticate(username, email)
            .subscribe(
                (data) => {
                    if(Object.values(data).length > 0) {
                        this.authService.login()
                        this.router.navigate(['users'])
                    } else {
                        this.loginForm.reset();
                        alert("Inválid User or Password!")
                    }
                },
                err => {
                    console.log(err);
                    this.loginForm.reset();
                    alert("Inválid User or Password!")
                }
            );
    }
}