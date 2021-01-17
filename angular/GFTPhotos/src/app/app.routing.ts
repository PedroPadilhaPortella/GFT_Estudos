import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login/login.component';
import { PhotoListComponent } from './photos/photo-list/photo-list.component';
import { UserListComponent } from './users/user-list/user-list.component';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'login'
    },
    { 
        path: 'login',
        component: LoginComponent
    },
    { 
        path: 'home',
        component: HomeComponent
    },
    { 
        path: 'users',
        component: UserListComponent
    },
    { 
        path: 'photos',
        component: PhotoListComponent
    },
];