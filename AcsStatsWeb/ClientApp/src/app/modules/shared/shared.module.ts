import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RecordsHeaderComponent} from "./components/records-header/records-header.component";
import {RouterModule} from "@angular/router";
import {PagingComponent} from "./components/paging/paging.component";
import { SearchHeaderComponent } from './search-header/search-header.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {PopoverModule, TooltipModule} from "@coreui/angular";


@NgModule({
  declarations: [RecordsHeaderComponent, PagingComponent, SearchHeaderComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    FontAwesomeModule,
    PopoverModule,
    TooltipModule
  ],
    exports: [RecordsHeaderComponent, PagingComponent, SearchHeaderComponent, PopoverModule, TooltipModule]
})
export class SharedModule {
}
