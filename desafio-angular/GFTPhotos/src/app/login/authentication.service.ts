import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
// import * as jwt_decode from 'jwt-decode';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    UserAPI = environment.usersUrl;
    userIsLogged = 'logado';

    constructor(private http: HttpClient) { }

    
    authenticate(username: string, email: string)
    {
        return this.http.get<any>(this.UserAPI + `?username=${username}&email=${email}`)
    }

    login() {
        localStorage.setItem("userLogged", this.userIsLogged);
        
    }
    
    logout() {
        localStorage.removeItem("userLogged");
    }
    
    isLogged() {
        return localStorage.getItem("userLogged") ? true : false
    }
}