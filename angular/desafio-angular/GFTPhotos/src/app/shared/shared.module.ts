import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "./components/header/header.component";
import { RouterModule } from "@angular/router"; 
import { SearchComponent } from "./components/search/search.component";
import { MessageComponent } from './components/message/message.component';

@NgModule({
    declarations: [
        HeaderComponent,
        SearchComponent,
        MessageComponent
    ],
    imports: [
        CommonModule,
        RouterModule
    ],
    exports: [
        HeaderComponent,
        SearchComponent,
        MessageComponent
    ]
})
export class SharedModule { }
