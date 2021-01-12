import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { routes } from './app.routing';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { APP_BASE_HREF } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { UsersModule } from './users/users.module';
import { PhotosModule } from './photos/photos.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeModule } from './home/home.module';
import { LoginModule } from './login/login.module';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        HttpClientModule,
        NgxPaginationModule,
        BrowserAnimationsModule,
        [RouterModule.forRoot(routes, { useHash: false })],
        LoginModule,
        UsersModule,
        PhotosModule,
        HomeModule,
        SharedModule,
        MatPaginatorModule,
    ],
    providers: [
        {
            provide: APP_BASE_HREF,
            useValue: '/'
        },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
