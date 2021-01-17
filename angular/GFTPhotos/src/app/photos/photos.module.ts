import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotoListComponent } from './photo-list/photo-list.component';
import { CardComponent } from './card/card.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { FilterByDescriptionPipe } from './filter-by-description.pipe';
import { DarkenOnHoverDirective } from './darken-on-hover.directive';
import { SharedModule } from '../shared/shared.module';
import { DialogComponent } from './dialog/dialog.component';
import { NgxSpinnerModule } from 'ngx-spinner';



@NgModule({
    imports: [
        CommonModule,
        NgxPaginationModule,
        SharedModule,
        NgxSpinnerModule
       
    ],
    declarations: [
        PhotoListComponent,
        CardComponent,
        FilterByDescriptionPipe,
        DarkenOnHoverDirective,
        DialogComponent,
    ],
    exports: [
        PhotoListComponent,
        CardComponent,
        DarkenOnHoverDirective
    ],
    entryComponents : [

    ]
})
export class PhotosModule { }
