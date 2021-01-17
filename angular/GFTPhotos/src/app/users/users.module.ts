import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { SharedModule } from '../shared/shared.module';
import { FilterUserPipe } from './user-list/filter-user.pipe';
import { RouterModule } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { DialogComponent } from './dialog/dialog.component';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
    imports: [
        CommonModule,
        NgxPaginationModule,
        SharedModule,
        RouterModule,
        MatTableModule,
        MatPaginatorModule,
        MatDialogModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatSortModule,
    ],
    declarations: [
        UserListComponent,
        FilterUserPipe,
        DialogComponent,
    ],
    exports: []
})
export class UsersModule { }
