import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from './classes/user';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private userAPI = environment.usersUrl;
    
    constructor(private http: HttpClient) { }

    getUsers(): Observable<User[]> 
    {
        return this.http.get<User[]>(this.userAPI);
    }

}
